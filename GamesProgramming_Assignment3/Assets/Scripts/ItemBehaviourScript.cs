using UnityEngine;

public class ItemBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance?.AddBag();
        ItemBank.Instance?.AddItems(1);
        Destroy(gameObject);
    }
}
