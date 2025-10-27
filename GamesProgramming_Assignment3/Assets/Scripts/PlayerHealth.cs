using UnityEngine;
using TMPro;
using System; // for Action
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public TMP_Text healthText;                 // optional numeric UI

    [Header("Audio")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    [Header("Damage Feedback")]
    public float flashDuration = 0.2f;

    public int CurrentHealth { get; private set; }
    public event Action<int, int> OnHealthChanged; // (current, max)

    // Cached comps (must be on this same GameObject)
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sr;

    bool isDead = false;

    void Start()
    {
        CurrentHealth = maxHealth;
        rb  = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr  = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();
        Notify();
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - dmg);
        Notify();

        if (hitSound != null && audioSource != null)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(hitSound);
        }

        // Flash red
        if (sr != null)
            StartCoroutine(FlashRed(flashDuration));

        if (CurrentHealth <= 0)
            Die();
    }

    private IEnumerator FlashRed(float duration)
    {
        Color originalColor = sr.color;
        sr.color = Color.red;           // turn red
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;       // revert to original
    }

    void Notify()
    {
        if (healthText) healthText.text = CurrentHealth.ToString();
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        // clamp & notify UI (ensures 0 on-screen)
        CurrentHealth = 0;
        Notify();

        // hard-stop player interaction but keep object alive for camera, UI, etc.
        if (rb)  rb.simulated = false;
        if (col) col.enabled  = false;
        if (sr)  sr.enabled   = false;

        // Tell the game flow to show Lose panel
        GameManager.Instance?.Lose();
        // DO NOT Destroy(gameObject);  // keeping it prevents camera null refs
    }

    // --- Hazard handling (works for both trigger and non-trigger setups) ---

    void OnCollisionEnter2D(Collision2D c)
    {
        if (isDead) return;

        if (c.collider.CompareTag("Spike"))     TakeDamage(1);
        if (c.collider.CompareTag("DeathZone")) Die();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (isDead) return;

        if (c.CompareTag("Spike"))     TakeDamage(1);
        if (c.CompareTag("DeathZone")) Die();
    }

    // --- Optional helpers if you add a respawn system later ---

    public void ReviveAt(Vector3 position)
    {
        // Call this if you implement a manual respawn (not needed for scene reload)
        isDead = false;
        CurrentHealth = maxHealth;
        Notify();

        transform.position = position;

        if (rb)  rb.simulated = true;
        if (col) col.enabled  = true;
        if (sr)  sr.enabled   = true;
    }
}
