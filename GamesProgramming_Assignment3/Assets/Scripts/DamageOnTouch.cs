using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public int damage = 1;       
    public bool destroyOnHit = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var hp = other.GetComponent<PlayerHealth>();
        if (hp != null) hp.TakeDamage(damage);

        if (destroyOnHit) Destroy(gameObject);
    }
}
