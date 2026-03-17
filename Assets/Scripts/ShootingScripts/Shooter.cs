using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private ARControls controls;
    private bool canShoot = false;
    public GameManager gameManager;

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

    public void EnableShooting()
    {
        canShoot = true;
    }

    void Shoot(InputAction.CallbackContext context)
    {
        if (!canShoot) return;

        // Shoot straight from camera center
        Ray ray = new Ray(
            Camera.main.transform.position,
            Camera.main.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.CompareTag("Target"))
            {
                gameManager.TargetDestroyed();
                Destroy(hit.transform.gameObject);
            }
        }
    }
}