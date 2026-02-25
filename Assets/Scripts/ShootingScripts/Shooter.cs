using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private ARControls controls;

    void Awake()
    {
        controls = new ARControls();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Tap.performed += Shoot;
    }

    void OnDisable()
    {
        controls.Gameplay.Tap.performed -= Shoot;
        controls.Gameplay.Disable();
    }

    void Shoot(InputAction.CallbackContext context)
    {
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Target"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}