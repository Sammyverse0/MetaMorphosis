using UnityEngine;
using Vuforia;

/// <summary>
/// Attach to the same GameObject as your Vuforia Image Target (ObserverBehaviour).
/// Controls the Lock prefab's Canvas visibility based on tracking state.
///
/// Setup:
///   1. Attach this script to your ImageTarget GameObject.
///   2. Drag your Lock's Canvas GameObject into the `lockCanvas` field.
///   3. Make sure the Canvas is a child of the ImageTarget (or the Lock prefab
///      that is a child of it) — either works.
/// </summary>
public class VuforiaLockUIVisibility : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The World-Space Canvas that holds all disk buttons and digit labels.")]
    public Canvas lockCanvas;

    [Tooltip("(Optional) Root lock GameObject to activate/deactivate entirely.")]
    public GameObject lockRoot;

    // ── Vuforia Observer ──────────────────────────────────────────────────────
    private ObserverBehaviour observer;

    // ─────────────────────────────────────────────────────────────────────────
    void Awake()
    {
        observer = GetComponent<ObserverBehaviour>();

        if (observer == null)
        {
            Debug.LogError("[VuforiaLockUIVisibility] No ObserverBehaviour found on this GameObject. " +
                           "Make sure this script is on your ImageTarget.");
            return;
        }

        // Hide everything immediately — before Vuforia fires its first event
        SetVisible(false);
    }

    // ─────────────────────────────────────────────────────────────────────────
    void OnEnable()
    {
        if (observer != null)
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnDisable()
    {
        if (observer != null)
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        bool isTracked = status.Status == Status.TRACKED
                      || status.Status == Status.EXTENDED_TRACKED
                      || status.Status == Status.LIMITED;

        SetVisible(isTracked);
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void SetVisible(bool visible)
    {
        // Show / hide the UI canvas
        if (lockCanvas != null)
            lockCanvas.gameObject.SetActive(visible);

        // Optionally activate the whole lock root (mesh, colliders, etc.)
        if (lockRoot != null)
            lockRoot.SetActive(visible);
    }
}