using UnityEngine;

public class Player : MonoBehaviour
{
    // Parameter
    [Range(0, 10)]
    [SerializeField] float speed = 4.0f;

    [Range(0, 10)]
    [SerializeField] float jumpForce = 6.0f;


    // Components
    [SerializeField] Transform tr;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] UIManager uIManager;

    // Internal Parameter
    private Vector2 scaleThisObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scaleThisObject = tr.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            // Move the player
            float moveInput = Input.GetAxisRaw("Horizontal");
            Vector2 moveVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

            if (moveInput * speed < 0)
            {
                this.tr.localScale = new Vector3(-scaleThisObject.x, scaleThisObject.y); // Flip the player
            }
            else
            {
                this.tr.localScale = new Vector3(scaleThisObject.x, scaleThisObject.y);
            }

            rb.linearVelocity = moveVelocity;

            anim.SetBool("isMove", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isMove", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Jump
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("JumpTrigger");
        }

        if (Input.GetButtonDown("Vertical") == true)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("JumpTrigger");
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player took " + damage + " damage!");
        uIManager.displayTakeDamage(damage);
    }
}
