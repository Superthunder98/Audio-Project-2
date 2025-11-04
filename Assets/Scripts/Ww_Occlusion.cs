using UnityEngine;

/// <summary>
/// Handles sound occlusion effects using Wwise, adjusting volume and low-pass filter based on obstacles between audio source and listener
/// </summary>
public class Ww_Occlusion : MonoBehaviour
{
    #region Serialized Fields
    [Header("References")]
    [SerializeField] private GameObject m_Audiolistener;

    [Header("Occlusion Settings")]
    [SerializeField] private bool m_UseOcclusion = false;
    [SerializeField] private LayerMask m_OccludeLayer;
    [SerializeField, Range(0.1f, 1f)] private float m_CheckInterval = 0.2f;
    [SerializeField] private bool m_UseDebug = false;

    [Header("Wwise RTPC Settings")]
    [SerializeField] private string m_RtpcLoPass = "RTPC_Occlusion_LoPass";
    [SerializeField] private string m_RtpcVolume = "RTPC_Occlusion_Volume";
    [SerializeField, Range(0f, 1f)] private float m_LoPassMax = 1f;
    [SerializeField, Range(0f, 1f)] private float m_VolumeMax = 1f;
    [SerializeField, Range(0f, 1f)] private float m_StartingOcclusionVolume = 0f;
    #endregion

    #region Private Fields
    private float m_Timer;
    private Vector3 m_LastHitPoint;
    private bool m_IsOccluded;
    private Transform m_CachedTransform;
    private Transform m_ListenerTransform;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        m_CachedTransform = transform;
        InitializeOcclusion();
    }

    private void OnEnable()
    {
        // Perform initial occlusion check when enabled
        PerformOcclusionCheck();
    }

    private void Update()
    {
        if (!ShouldCheckOcclusion()) return;

        m_Timer += Time.deltaTime;
        if (m_Timer >= m_CheckInterval)
        {
            m_Timer = 0f;
            PerformOcclusionCheck();
        }

        if (m_UseDebug)
        {
            DrawDebugLines();
        }
    }

    private void OnValidate()
    {
        m_CheckInterval = Mathf.Max(0.1f, m_CheckInterval);
    }
    #endregion

    #region Private Methods
    private void InitializeOcclusion()
    {
        if (m_OccludeLayer == 0)
        {
            m_OccludeLayer = LayerMask.GetMask("Occlude Sound");
        }

        if (m_Audiolistener != null)
        {
            m_ListenerTransform = m_Audiolistener.transform;
        }
        else
        {
            Debug.LogWarning($"[Ww_Occlusion] No audio listener assigned to {gameObject.name}", this);
            enabled = false;
            return;
        }

        // Apply initial RTPC values
        SetRtpcValues(m_StartingOcclusionVolume > 0);
    }

    private bool ShouldCheckOcclusion()
    {
        return m_UseOcclusion && m_Audiolistener != null;
    }

    private void PerformOcclusionCheck()
    {
        Vector3 listenerPosition = m_ListenerTransform.position;
        Vector3 emitterPosition = m_CachedTransform.position;

        m_IsOccluded = Physics.Linecast(listenerPosition, emitterPosition, out RaycastHit hit, m_OccludeLayer) 
            && hit.collider.gameObject != gameObject;

        if (m_IsOccluded)
        {
            m_LastHitPoint = hit.point;
        }

        SetRtpcValues(m_IsOccluded);
    }

    private void SetRtpcValues(bool _isOccluded)
    {
        float loPassValue = _isOccluded ? m_LoPassMax : 0f;
        float volumeValue = _isOccluded ? m_VolumeMax : 0f;

        AkSoundEngine.SetRTPCValue(m_RtpcLoPass, loPassValue, gameObject);
        AkSoundEngine.SetRTPCValue(m_RtpcVolume, volumeValue, gameObject);
    }

    private void DrawDebugLines()
    {
        Vector3 listenerPosition = m_ListenerTransform.position;
        Vector3 emitterPosition = m_CachedTransform.position;
        
        if (m_IsOccluded)
        {
            Debug.DrawLine(listenerPosition, m_LastHitPoint, Color.red);
            Debug.DrawLine(m_LastHitPoint, emitterPosition, Color.yellow);
        }
        else
        {
            Debug.DrawLine(listenerPosition, emitterPosition, Color.green);
        }
    }
    #endregion
}
