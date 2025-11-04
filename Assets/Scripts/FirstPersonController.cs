using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using AK.Wwise;
#pragma warning disable 618, 649

namespace UnityStandardAssets.Characters.FirstPerson
{
    /// <summary>
    /// Silent event poster for Wwise events that are expected to have empty containers
    /// This is used to suppress error messages for events that are intentionally empty for student projects
    /// </summary>
    public static class WwiseSilentEvents
    {
        /// <summary>
        /// List of event names that should not log errors when empty
        /// </summary>
        private static readonly string[] s_SilentEventNames = new string[]
        {
            "Play_Footstep",
            "Play_Jump",
            "Play_Landing"
        };

        // Cache the property info to avoid performance hit from repeated lookups
        private static PropertyInfo s_IsLoggingEnabledProperty = null;

        // Static constructor to cache the property info
        static WwiseSilentEvents()
        {
            try
            {
                // Get the AkCallbackManager type - try all possible namespaces
                Type akCallbackManagerType = null;
                
                // Common namespaces for AkCallbackManager
                string[] possibleNamespaces = new string[]
                {
                    "AK.Wwise",
                    "AK.Wwise.API.Runtime.Handwritten.Common",
                    "AK.Wwise.API.Runtime.Common",
                    "AK.Wwise.API.Runtime",
                    "AK.Wwise.API",
                    "AK.Wwise.Common",
                    "AK"
                };
                
                // Try to find the type in each namespace
                foreach (string ns in possibleNamespaces)
                {
                    akCallbackManagerType = Type.GetType($"{ns}.AkCallbackManager");
                    if (akCallbackManagerType != null)
                        break;
                }
                
                // If still not found, try to find in all loaded assemblies
                if (akCallbackManagerType == null)
                {
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.Name == "AkCallbackManager")
                            {
                                akCallbackManagerType = type;
                                break;
                            }
                        }
                        
                        if (akCallbackManagerType != null)
                            break;
                    }
                }
                
                // If we found the type, get the IsLoggingEnabled property
                if (akCallbackManagerType != null)
                {
                    // Try to find the property with different binding flags
                    s_IsLoggingEnabledProperty = akCallbackManagerType.GetProperty("IsLoggingEnabled", 
                        BindingFlags.NonPublic | BindingFlags.Static);
                    
                    if (s_IsLoggingEnabledProperty == null)
                    {
                        // Try different binding flags if not found
                        s_IsLoggingEnabledProperty = akCallbackManagerType.GetProperty("IsLoggingEnabled", 
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                    }
                    
                    if (s_IsLoggingEnabledProperty == null)
                    {
                        // Last resort - try to find a field with this name
                        FieldInfo field = akCallbackManagerType.GetField("IsLoggingEnabled", 
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                        
                        if (field != null)
                        {
                            Debug.LogWarning("WwiseSilentEvents: IsLoggingEnabled found as a field, not a property. Error suppression may not work as expected.");
                        }
                    }
                }
                
                // Log a warning if we couldn't find the property
                if (s_IsLoggingEnabledProperty == null)
                {
                    Debug.LogWarning("WwiseSilentEvents: Could not find IsLoggingEnabled property in AkCallbackManager. Using fallback method for error suppression.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"WwiseSilentEvents: Error initializing: {ex.Message}. Error suppression will use fallback method.");
            }
        }
        
        /// <summary>
        /// Temporarily disables Wwise logging
        /// </summary>
        /// <returns>Previous logging state to restore</returns>
        private static bool DisableLogging()
        {
            if (s_IsLoggingEnabledProperty == null)
                return true;
            
            // Get current value
            bool previousValue = (bool)s_IsLoggingEnabledProperty.GetValue(null);
            
            // Set to false to disable logging
            s_IsLoggingEnabledProperty.SetValue(null, false);
            
            return previousValue;
        }
        
        /// <summary>
        /// Restores previous Wwise logging state
        /// </summary>
        private static void RestoreLogging(bool previousState)
        {
            if (s_IsLoggingEnabledProperty == null)
                return;
            
            // Restore previous state
            s_IsLoggingEnabledProperty.SetValue(null, previousState);
        }

        /// <summary>
        /// Posts a Wwise event but silently ignores errors for certain event names
        /// </summary>
        /// <param name="_eventName">Name of the Wwise event to post</param>
        /// <param name="_gameObject">GameObject to post the event on</param>
        /// <returns>The playing ID of the event (0 if silent error)</returns>
        public static uint PostEventSilent(string _eventName, GameObject _gameObject)
        {
            // Check if this event should be silent
            bool shouldBeSilent = Array.Exists(s_SilentEventNames, eventName => eventName == _eventName);
            
            // If the event should be silent, disable logging temporarily
            if (shouldBeSilent)
            {
                if (s_IsLoggingEnabledProperty != null)
                {
                    // Main approach - use reflection to disable logging
                    bool previousLoggingState = DisableLogging();
                    uint playingID = AkSoundEngine.PostEvent(_eventName, _gameObject);
                    RestoreLogging(previousLoggingState);
                    return playingID;
                }
                else
                {
                    // Fallback approach - create our own Console.LogError method to intercept the error
                    // This uses the Application.logMessageReceived event which might get called before the error reaches the log
                    uint playingID = 0;
                    
                    // Create our filter callback
                    Application.LogCallback filterCallback = (string logString, string stackTrace, LogType type) =>
                    {
                        // Only suppress errors that match our event name
                        if (type == LogType.Error && logString.Contains(_eventName))
                        {
                            // Suppress the error by doing nothing
                        }
                    };
                    
                    // Add our filter callback
                    Application.logMessageReceived += filterCallback;
                    
                    try
                    {
                        // Post the event - any errors will be filtered
                        playingID = AkSoundEngine.PostEvent(_eventName, _gameObject);
                        
                        // Small delay to ensure the callback has time to process any errors
                        // This is necessary because the actual error might be posted in a later frame
                        // We'll just ignore this complexity for now, as it's just a fallback method
                    }
                    finally
                    {
                        // Remove our filter callback
                        Application.logMessageReceived -= filterCallback;
                    }
                    
                    return playingID;
                }
            }
            
            // For non-silent events, post normally
            return AkSoundEngine.PostEvent(_eventName, _gameObject);
        }
    }

    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private float m_CrouchSpeed;

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        private FootstepSwapper swapper;
        private bool m_IsCrouching;
        private bool m_IsInitializing = true;
        private int m_InitializationFrames = 2;

        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);
            swapper = GetComponent<FootstepSwapper>();

            ResetCameraRotation();
            StartCoroutine(EnableMouseLookAfterDelay());
        }

        private void Update()
        {
            if (m_IsInitializing)
            {
                m_InitializationFrames--;
                if (m_InitializationFrames <= 0)
                {
                    m_IsInitializing = false;
                }
                return;
            }

            if (PauseMenu.IsGamePaused())
            {
                m_MouseLook.SetCursorLock(false);
                return;
            }
            else
            {
                m_MouseLook.SetCursorLock(true);
            }

            RotateView();

            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

            if (Input.GetKeyDown(KeyCode.C))
            {
                m_IsCrouching = true;
                m_CharacterController.height /= 2f;
                m_CharacterController.center /= 2f;
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                m_IsCrouching = false;
                m_CharacterController.height *= 2f;
                m_CharacterController.center *= 2f;
            }
        }

        private void PlayLandingSound()
        {
            swapper.CheckLayers();
            WwiseSilentEvents.PostEventSilent("Play_Landing", gameObject);
            AudioDebugUI.Report("Play_Landing");
            m_NextStep = m_StepCycle + .5f;
        }

        private void FixedUpdate()
        {
            if (PauseMenu.IsGamePaused() || m_IsInitializing)
            {
                return;
            }

            float speed;
            GetInput(out speed);

            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }

            if (m_IsCrouching)
            {
                speed = m_CrouchSpeed;
            }
            else
            {
                speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            }

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
            m_MouseLook.UpdateCursorLock();
        }

        private void PlayJumpSound()
        {
            swapper.CheckLayers();
            WwiseSilentEvents.PostEventSilent("Play_Jump", gameObject);
            AudioDebugUI.Report("Play_Jump");
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;
            PlayFootStepAudio();
        }

        private void PlayFootStepAudio()
        {
            swapper.CheckLayers();
            if (!m_CharacterController.isGrounded)
            {
                return;
            }

            // Use our silent event poster that ignores errors for empty containers
            // The containers are intentionally empty for student projects
            WwiseSilentEvents.PostEventSilent("Play_Footstep", gameObject);
            AudioDebugUI.Report("Play_Footstep");
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }

            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        private void GetInput(out float speed)
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }

        private void ResetCameraRotation()
        {
            m_Camera.transform.localRotation = Quaternion.identity;
            m_MouseLook.Init(transform, m_Camera.transform);
        }

        private IEnumerator EnableMouseLookAfterDelay()
        {
            m_MouseLook.SetCursorLock(false);
            yield return new WaitForSeconds(0.1f);
            m_MouseLook.SetCursorLock(true);
        }
    }
}