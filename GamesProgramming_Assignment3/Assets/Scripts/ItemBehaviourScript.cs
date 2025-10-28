using UnityEngine;

public class ItemBehaviourScript : MonoBehaviour
{
     public AudioClip pickupSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound, 0.35f);
        }


        ItemBank.Instance?.AddItems(1);
        GameManager.Instance?.AddBag();

        var bar = FindFirstObjectByType<BagProgressBar>();
        if (bar != null)
        {
            bar.AddBag();
        }

        Destroy(gameObject, pickupSound ? pickupSound.length : 0f);
    }
}