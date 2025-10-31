// DamageOnTouch3D.cs  (on the Spike)
using UnityEngine;

public class DamageOnTouch3D : MonoBehaviour
{
    public int damage = 1;
    public bool destroyOnHit = false;

    bool _hasHit; // guard

    void OnTriggerEnter(Collider other)
    {
        if (_hasHit) return;                     // <-- stop double hit
        if (!other.CompareTag("Player")) return;

        var hp = other.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            _hasHit = true;
            hp.TakeDamage(damage);
        }

        if (destroyOnHit) Destroy(gameObject);
    }
}
