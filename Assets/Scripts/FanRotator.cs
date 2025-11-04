using UnityEngine;

/// <summary>
/// Controls the rotation of a ceiling fan with customizable speed settings.
/// </summary>
public class FanRotator : MonoBehaviour
{
    #region Private Fields
    [SerializeField, Range(0f, 100f)] private float m_RotationSpeed = 30f;
    [SerializeField] private Vector3 m_RotationAxis = Vector3.down; // Default rotation around Y-axis downward
    [SerializeField] private bool m_StartSpinningOnAwake = true;
    [SerializeField] private AnimationCurve m_SpeedRampCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    private bool m_IsSpinning;
    private float m_CurrentSpeedMultiplier = 1f;
    private Quaternion m_InitialRotation;
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the rotation speed in degrees per second.
    /// </summary>
    public float RotationSpeed
    {
        get => m_RotationSpeed;
        set => m_RotationSpeed = value;
    }

    /// <summary>
    /// Gets or sets whether the fan is currently spinning.
    /// </summary>
    public bool IsSpinning
    {
        get => m_IsSpinning;
        set => m_IsSpinning = value;
    }
    
    /// <summary>
    /// Gets or sets the speed multiplier (0-1 range).
    /// </summary>
    public float SpeedMultiplier
    {
        get => m_CurrentSpeedMultiplier;
        set => m_CurrentSpeedMultiplier = Mathf.Clamp01(value);
    }
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        m_InitialRotation = transform.rotation;
        m_IsSpinning = m_StartSpinningOnAwake;
    }

    private void Update()
    {
        if (m_IsSpinning)
        {
            // Calculate actual rotation speed using curve for more natural acceleration/deceleration
            float actualSpeed = m_RotationSpeed * m_SpeedRampCurve.Evaluate(m_CurrentSpeedMultiplier);
            
            // Apply rotation
            transform.Rotate(m_RotationAxis, actualSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Starts the fan spinning.
    /// </summary>
    public void StartSpinning()
    {
        m_IsSpinning = true;
    }

    /// <summary>
    /// Stops the fan from spinning.
    /// </summary>
    public void StopSpinning()
    {
        m_IsSpinning = false;
    }
    
    /// <summary>
    /// Set the fan speed to a specific percentage (0-1 range).
    /// </summary>
    /// <param name="_speedPercentage">Speed percentage between 0 and 1</param>
    public void SetSpeedPercentage(float _speedPercentage)
    {
        m_CurrentSpeedMultiplier = Mathf.Clamp01(_speedPercentage);
    }
    
    /// <summary>
    /// Resets the fan to its initial rotation.
    /// </summary>
    public void ResetRotation()
    {
        transform.rotation = m_InitialRotation;
    }
    #endregion
    
    #if UNITY_EDITOR
    /// <summary>
    /// Validates the rotation axis to ensure it's not zero.
    /// </summary>
    private void OnValidate()
    {
        if (m_RotationAxis == Vector3.zero)
        {
            Debug.LogWarning("Rotation axis should not be zero. Defaulting to Vector3.down.");
            m_RotationAxis = Vector3.down;
        }
    }
    #endif
} 