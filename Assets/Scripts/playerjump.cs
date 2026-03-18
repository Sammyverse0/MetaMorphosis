using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float jumpForce = 6f;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Jump();

        if (Mouse.current.leftButton.wasPressedThisFrame)
            Jump();

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            Jump();

        if (rb.linearVelocity.y > 0.1f)
            anim.SetBool("isFlying", true);
        else
            anim.SetBool("isFlying", false);
    }

    void Jump()
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;

        AudioManager.instance.PlayDeathSound();
        Invoke("RestartAfterDelay", 0.6f);
    }

    void RestartAfterDelay()
    {
        GameManager.instance.RestartGame();
    }
}