using UnityEngine;

public class ItemBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        ItemBank.Instance?.AddItems(1);
        GameManager.Instance?.AddBag();
        var bar = FindFirstObjectByType<BagProgressBar>();
        if (bar != null)
        {
            bar.AddBag();
        }

        Destroy(gameObject);
    }
}

 