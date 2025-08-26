using System.Collections;
using UnityEngine;

enum HeadRockState
{
    Idle,
    Falling,
    DelayIdle,
    Rising
}

public class HeadRock : MonoBehaviour
{
    [SerializeField] float delayStartTime = 0.0f;
    [SerializeField] float idleTime = 6.0f;
    [SerializeField] float delayIdleTime = 2.0f;
    [SerializeField] float fallSpeed = 0.01f;

    private Vector2 startPosition;
    private HeadRockState currentState;

    // 状態時間計測用
    private float stateTimer = 0f;

    void Awake()
    {
        startPosition = transform.position;

        StartCoroutine(DelayStartTime(delayStartTime));
        ChangeState(HeadRockState.Idle);
    }

    private IEnumerator DelayStartTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    void Update()
    {
        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case HeadRockState.Idle:
                if (stateTimer >= idleTime)
                    ChangeState(HeadRockState.Falling);
                break;

            case HeadRockState.Falling:
                transform.position += Vector3.down * fallSpeed * Time.deltaTime;
                break;

            case HeadRockState.DelayIdle:
                if (stateTimer >= delayIdleTime)
                    ChangeState(HeadRockState.Rising);
                break;

            case HeadRockState.Rising:
                transform.position += Vector3.up * fallSpeed * Time.deltaTime;
                if (Vector2.Distance(transform.position, startPosition) < 0.1f)
                {
                    transform.position = startPosition;
                    ChangeState(HeadRockState.Idle);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(1);
        }
        else if (other.CompareTag("Ground"))
        {
            if (currentState == HeadRockState.Falling)
                ChangeState(HeadRockState.DelayIdle);
        }
    }

    private void ChangeState(HeadRockState newState)
    {
        currentState = newState;
        stateTimer = 0f; // 状態開始時にタイマーリセット
        Debug.Log("State changed to: " + currentState);
    }
}
