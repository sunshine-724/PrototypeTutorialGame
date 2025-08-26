using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Parameter
    [Range(0, 10)]
    [SerializeField] float speed = 4.0f;

    [Range(0, 10)]
    [SerializeField] float jumpForce = 6.0f;

    [SerializeField] float timeDamageEffect = 1.0f;
    [SerializeField] float damageEffectCycle = 0.1f;


    // Components
    [SerializeField] Transform tr;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;

    [SerializeField] UIManager uIManager;

    // Internal Parameter
    private Vector2 scaleThisObject;
    private bool isInvincible = false;


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

    public IEnumerator TakeDamage(float damage)
    {
        Debug.Log("Player took " + damage + " damage!");
        if (isInvincible)
        {
            yield break;
        }
        isInvincible = true;
        uIManager.displayTakeDamage(damage);

        StartCoroutine(TakeDamageEffect());
        yield return new WaitForSeconds(timeDamageEffect);
        StopCoroutine(TakeDamageEffect());

        uIManager.hideUIText();
        isInvincible = false;
    }

    private IEnumerator TakeDamageEffect()
    {
        float _time = 0.0f;
        bool isClear = false;

        while (_time <= timeDamageEffect)
        {
            yield return new WaitForSeconds(damageEffectCycle);
            // Debug.Log("isClear:" + isClear);
            _time += damageEffectCycle;
            if (isClear)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }

            isClear = !isClear;
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
