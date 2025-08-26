using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] Transform source;
    [SerializeField] Transform target;
    [SerializeField] float speed = 5.0f;

    [SerializeField] Vector3 offset;

    void Update()
    {
        if (target != null)
        {
            source.position = target.position + offset;
        }
    }
}
