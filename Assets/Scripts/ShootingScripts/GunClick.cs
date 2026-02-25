using UnityEngine;
using UnityEngine.InputSystem;

public class GunClick : MonoBehaviour
{
    private ARControls controls;
    public GameManager gameManager;

    void Awake()
    {
        controls = new ARControls();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Tap.performed += OnTap;
    }

    void OnDisable()
    {
        controls.Gameplay.Tap.performed -= OnTap;
        controls.Gameplay.Disable();
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                gameManager.StartGame();
            }
        }
    }
}