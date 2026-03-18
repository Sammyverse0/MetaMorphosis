using UnityEngine;
using UnityEngine.InputSystem;

public class GunClick : MonoBehaviour
{
    private ARControls controls;
    public GameManager gameManager;

    //public Transform worldAnchor;

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
                // Remove callback FIRST
                controls.Gameplay.Tap.performed -= OnTap;

                // Start game
                gameManager.StartGame();

                // Disable gun
                gameObject.SetActive(false);
            }
        }
    }
}