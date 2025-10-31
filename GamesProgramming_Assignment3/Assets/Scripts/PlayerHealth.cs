using UnityEngine;
using TMPro;
using System;
using System.Collections;
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
    public float flashDuration = 0.12f;
    [Tooltip("Prevents multiple hits in the same overlap")]
    public float hitCooldown = 0.2f;


    public float flashDuration = 0.2f;

    public int CurrentHealth { get; private set; }
    public event Action<int, int> OnHealthChanged;

    // Physics (3D)
    Rigidbody rb3D;
    Collider col3D;

    // Visuals
    Renderer[] meshRenderers;           // 3D (Mesh/SkinnedMesh)
    SpriteRenderer[] spriteRenderers;   // 2D
    MaterialPropertyBlock mpb;
    static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    bool isDead = false;
    bool recentlyHit = false;

    void Start()
    {
        CurrentHealth = maxHealth;
        rb  = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr  = GetComponent<SpriteRenderer>();

        rb3D = GetComponent<Rigidbody>();
        col3D = GetComponent<Collider>();

        meshRenderers = GetComponentsInChildren<Renderer>(true);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        mpb = new MaterialPropertyBlock();

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();

        Notify();
    }

    public void TakeDamage(int dmg)
{
    if (isDead) return;

    int finalDamage = dmg; // default 1

    if (DifficultyManager.Instance)
    {
        if (isDead || recentlyHit) return;
        StartCoroutine(HitCooldown());

        if (isDead || recentlyHit) return;
        StartCoroutine(HitCooldown());
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

        if (hitSound) audioSource.PlayOneShot(hitSound);

        StartCoroutine(FlashRed(flashDuration));

        if (CurrentHealth <= 0) Die();
    }

    IEnumerator HitCooldown()
    {
        recentlyHit = true;
        yield return new WaitForSeconds(hitCooldown);
        recentlyHit = false;
    }

    IEnumerator FlashRed(float duration)
    {
        // --- 3D renderers (URP Lit) ---
        var savedMeshColors = new Color[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            var r = meshRenderers[i];
            if (!r) continue;

            // try _BaseColor; if missing, fall back to material.color read
            Color original =
                r.sharedMaterial != null && r.sharedMaterial.HasProperty(BaseColor)
                ? r.sharedMaterial.GetColor(BaseColor)
                : r.material.color;

            savedMeshColors[i] = original;

            var flash = Color.Lerp(original, new Color(1f, 0.25f, 0.25f), 0.6f);
            r.GetPropertyBlock(mpb);
            mpb.SetColor(BaseColor, flash);
            r.SetPropertyBlock(mpb);
        }

        // --- 2D sprite renderers ---
        var savedSpriteColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            var sr = spriteRenderers[i];
            if (!sr) continue;
            savedSpriteColors[i] = sr.color;
            sr.color = Color.Lerp(sr.color, new Color(1f, 0.25f, 0.25f, sr.color.a), 0.6f);
        }

        yield return new WaitForSeconds(duration);

        // restore 3D
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            var r = meshRenderers[i];
            if (!r) continue;
            r.GetPropertyBlock(mpb);
            mpb.SetColor(BaseColor, savedMeshColors[i]);
            r.SetPropertyBlock(mpb);
        }

        // restore 2D
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            var sr = spriteRenderers[i];
            if (!sr) continue;
            sr.color = savedSpriteColors[i];
        }
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

        CurrentHealth = 0;
        Notify();

        if (rb3D) rb3D.isKinematic = true;
        if (col3D) col3D.enabled = false;

        GameManager.Instance?.Lose();
    }

    // ---------- 3D PHYSICS ----------
    void OnTriggerEnter(Collider c)
    {
        if (isDead) return;
        if (c.CompareTag("Spike")) TakeDamage(1);
        if (c.CompareTag("DeathZone")) Die();
    }
    void OnCollisionEnter(Collision c)
    {
        if (isDead) return;
        if (c.collider.CompareTag("Spike")) TakeDamage(1);
        if (c.collider.CompareTag("DeathZone")) Die();
    }

    // ---------- 2D PHYSICS (kept for your 2D levels) ----------
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
