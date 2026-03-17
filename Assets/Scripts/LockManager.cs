using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this to the root Lock GameObject.
/// Holds the secret combination, wires up all six buttons, and checks for a win.
/// </summary>
public class LockManager : MonoBehaviour
{
    // ── Disk References ───────────────────────────────────────────────────────
    [Header("Disk Controllers (Left → Right)")]
    public DiskController disk0;   // left   disk
    public DiskController disk1;   // middle disk
    public DiskController disk2;   // right  disk

    // ── Buttons ───────────────────────────────────────────────────────────────
    [Header("Buttons — Disk 0")]
    public Button disk0LeftBtn;
    public Button disk0RightBtn;

    [Header("Buttons — Disk 1")]
    public Button disk1LeftBtn;
    public Button disk1RightBtn;

    [Header("Buttons — Disk 2")]
    public Button disk2LeftBtn;
    public Button disk2RightBtn;

    // ── Secret Combination ────────────────────────────────────────────────────
    [Header("Secret Combination")]
    [Range(0, 9)] public int correctDigit0 = 3;
    [Range(0, 9)] public int correctDigit1 = 7;
    [Range(0, 9)] public int correctDigit2 = 1;

    // ── State ─────────────────────────────────────────────────────────────────
    private bool lockSolved = false;

    // ─────────────────────────────────────────────────────────────────────────
    void Awake()
    {
        WireButtons();
    }

    void OnEnable()
    {
        // Reset everything whenever the lock spawns / becomes active
        ResetAllDisks();
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void WireButtons()
    {
        // Disk 0
        disk0LeftBtn .onClick.AddListener(() => HandlePress(disk0, false));
        disk0RightBtn.onClick.AddListener(() => HandlePress(disk0, true ));

        // Disk 1
        disk1LeftBtn .onClick.AddListener(() => HandlePress(disk1, false));
        disk1RightBtn.onClick.AddListener(() => HandlePress(disk1, true ));

        // Disk 2
        disk2LeftBtn .onClick.AddListener(() => HandlePress(disk2, false));
        disk2RightBtn.onClick.AddListener(() => HandlePress(disk2, true ));
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void HandlePress(DiskController disk, bool isRight)
    {
        if (lockSolved) return;          // freeze input after win

        if (isRight) disk.PressRight();
        else         disk.PressLeft();

        CheckCombination();
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void CheckCombination()
    {
        bool correct = disk0.CurrentDigit == correctDigit0
                    && disk1.CurrentDigit == correctDigit1
                    && disk2.CurrentDigit == correctDigit2;

        if (correct)
        {
            lockSolved = true;
            OnLockOpened();
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    private void OnLockOpened()
    {
        Debug.Log("Lock opened! ✔");
        // TODO (next step): play open animation, show win UI, trigger hint reveal, etc.
    }

    // ─────────────────────────────────────────────────────────────────────────
    public void ResetAllDisks()
    {
        lockSolved = false;
        disk0?.ResetDisk();
        disk1?.ResetDisk();
        disk2?.ResetDisk();
    }

    // ── Editor helper: lets you verify the combo in Play Mode via context menu ─
    [ContextMenu("Debug / Log Current Digits")]
    private void LogDigits()
    {
        Debug.Log($"Current combo: {disk0.CurrentDigit} - {disk1.CurrentDigit} - {disk2.CurrentDigit}  |  Correct: {correctDigit0}-{correctDigit1}-{correctDigit2}");
    }
}