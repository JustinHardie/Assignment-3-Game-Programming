using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public TMP_Text healthText;

    [Header("Audio")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    [Header("Damage Feedback")]
    [Tooltip("How long the red flash lasts")]
    public float flashDuration = 0.2f;
    [Tooltip("Prevents multiple hits in the same overlap")]
    public float hitCooldown = 0.2f;

    public int CurrentHealth { get; private set; }
    public event Action<int, int> OnHealthChanged;

    // Physics (3D)
    Rigidbody rb3D;
    Collider col3D;

    // Visuals
    SpriteRenderer sr;
    Renderer[] meshRenderers;
    SpriteRenderer[] spriteRenderers;
    MaterialPropertyBlock mpb;
    static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    bool isDead = false;
    bool recentlyHit = false;

    void Start()
    {
        CurrentHealth = maxHealth;

        rb3D = GetComponent<Rigidbody>();
        col3D = GetComponent<Collider>();
        sr = GetComponent<SpriteRenderer>();

        meshRenderers = GetComponentsInChildren<Renderer>(true);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        mpb = new MaterialPropertyBlock();

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        Notify();
    }

    public void TakeDamage(int dmg)
    {
        if (isDead || recentlyHit) return;
        StartCoroutine(HitCooldown());

        int finalDamage = dmg;

        if (DifficultyManager.Instance)
        {
            switch (DifficultyManager.Instance.Current)
            {
                case Difficulty.Easy:
                    finalDamage = 1; // 3 hits to die
                    break;
                case Difficulty.Medium:
                    finalDamage = 2; // 2 hits to die
                    break;
                case Difficulty.Hard:
                    finalDamage = 3; // 1 hit to die
                    break;
            }
        }

        CurrentHealth = Mathf.Max(0, CurrentHealth - finalDamage);
        Notify();

        // Play hit sound
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

    IEnumerator HitCooldown()
    {
        recentlyHit = true;
        yield return new WaitForSeconds(hitCooldown);
        recentlyHit = false;
    }

    IEnumerator FlashRed(float duration)
    {
        Color originalColor = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;
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
        CurrentHealth = 0;
        Notify();

        if (rb3D) rb3D.isKinematic = true;
        if (col3D) col3D.enabled = false;

        GameManager.Instance?.Lose();
    }

    // ---------- 2D PHYSICS ----------
    void OnTriggerEnter2D(Collider2D c)
    {
        if (isDead) return;
        if (c.CompareTag("Spike")) TakeDamage(1);
        if (c.CompareTag("DeathZone")) Die();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (isDead) return;
        if (c.collider.CompareTag("Spike")) TakeDamage(1);
        if (c.collider.CompareTag("DeathZone")) Die();
    }
}
