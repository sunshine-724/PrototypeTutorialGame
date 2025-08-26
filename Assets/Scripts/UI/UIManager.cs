using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Reference to the player
    [SerializeField] private TMPro.TextMeshProUGUI text;

    // Update is called once per frame
    public void displayTakeDamage(float damage)
    {
        text.text = "Playerは" + damage + "ダメージを受けた!";
    }
}
