using UnityEngine;

public class DamageOnTouch3D : MonoBehaviour
{
    public int damage = 1;
    public bool destroyOnHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var hp = other.GetComponent<PlayerHealth>();
        if (hp != null) hp.TakeDamage(damage);

        if (destroyOnHit) Destroy(gameObject);
    }
}
