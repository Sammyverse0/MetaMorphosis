using UnityEngine;
using UnityEngine.InputSystem;

public class WireClick : MonoBehaviour
{
    public BombGameManager manager;

    void Update()
    {
        // Mouse click (Editor)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryRaycast(Mouse.current.position.ReadValue());
        }

        // Touch input (Mobile)
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            TryRaycast(Touchscreen.current.primaryTouch.position.ReadValue());
        }
    }

    void TryRaycast(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                Debug.Log("Wire clicked: " + gameObject.name);
                manager.CutWire(gameObject.name);
            }
        }
    }
}
