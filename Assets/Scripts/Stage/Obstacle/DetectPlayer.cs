using UnityEngine;

public class DetectPlayer : MonoBehaviour {
    [SerializeField] private int damageValue = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("triggerEnter: " + other.tag);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damageValue);
        }
    }
}