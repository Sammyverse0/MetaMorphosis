using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Attach this to each rotating disk GameObject on the padlock.
/// Controls one disk's digit (0–9) and its Y-axis rotation.
/// </summary>
public class DiskController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Degrees the disk rotates per button press")]
    public float degreesPerStep = 35f;

    [Tooltip("Duration of the rotation animation in seconds")]
    public float rotateDuration = 0.2f;

    [Header("UI")]
    [Tooltip("TextMeshPro label that shows the current digit on this disk")]
    public TextMeshProUGUI digitLabel;

    // ── State ────────────────────────────────────────────────────────────────
    private int   currentDigit   = 0;
    private bool  isAnimating     = false;

    // ── Public read-only access for LockManager ──────────────────────────────
    public int CurrentDigit => currentDigit;

    // ─────────────────────────────────────────────────────────────────────────
    void Start()
    {
        UpdateLabel();
    }

    // ─────────────────────────────────────────────────────────────────────────
    /// <summary>Called by the LEFT button. Decreases digit and rotates -35°.</summary>
    public void PressLeft()
    {
        if (isAnimating) return;

        // Digit wraps: 0 → 9
        currentDigit = (currentDigit - 1 + 10) % 10;
        UpdateLabel();
        StartCoroutine(RotateDisk(-degreesPerStep));
    }

    public void PressRight()
    {
        if (isAnimating) return;

        // Digit wraps: 9 → 0
        currentDigit = (currentDigit + 1) % 10;
        UpdateLabel();
        StartCoroutine(RotateDisk(degreesPerStep));
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void UpdateLabel()
    {
        if (digitLabel != null)
            digitLabel.text = currentDigit.ToString();
    }

    private IEnumerator RotateDisk(float deltaY)
    {
        isAnimating = true;

        Quaternion startRot = transform.localRotation;
        Quaternion endRot   = startRot * Quaternion.Euler(0f, deltaY, 0f);

        float elapsed = 0f;
        while (elapsed < rotateDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / rotateDuration);
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        transform.localRotation = endRot;
        isAnimating = false;
    }

    public void ResetDisk()
    {
        StopAllCoroutines();
        isAnimating   = false;
        currentDigit  = 0;
        transform.localRotation = Quaternion.identity;
        UpdateLabel();
    }
}