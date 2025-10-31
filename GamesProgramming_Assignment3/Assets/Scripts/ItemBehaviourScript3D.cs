using UnityEngine;

public class ItemBehaviourScript3D : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
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

        // hide visual immediately but keep sound
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, pickupSound ? pickupSound.length : 0f);
    }
}
