using UnityEngine;

public class MoveOnLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float loopTime = 10.0f;

    private int currentLineIndex = 0;
    private Vector2 targetPosition;
    private float speed;

    private void Start()
    {
        // 全長を計算
        float totalLength = 0f;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector2 p1 = lineRenderer.GetPosition(i);
            Vector2 p2 = lineRenderer.GetPosition((i + 1) % lineRenderer.positionCount);
            totalLength += Vector2.Distance(p1, p2);
        }

        // 全長 / 時間 = 速度
        speed = totalLength / loopTime;

        // 最初のターゲット設定
        targetPosition = lineRenderer.GetPosition((currentLineIndex + 1) % lineRenderer.positionCount);
    }

     private void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // ターゲット到達したら次へ
        if (Vector2.Distance(transform.position, targetPosition) < 0.001f)
        {
            currentLineIndex = (currentLineIndex + 1) % lineRenderer.positionCount;
            targetPosition = lineRenderer.GetPosition((currentLineIndex + 1) % lineRenderer.positionCount);
        }
    }
}
