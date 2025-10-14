using UnityEngine;

public class OilBarrel : MonoBehaviour
{
    public GameObject oilEffectPrefab;
    public float effectDuration = 3f;
    public Transform canvasTransform;

    private void Start()
    {
        if (canvasTransform == null)
        {
            Canvas c = FindObjectOfType<Canvas>();
            if (c != null) canvasTransform = c.transform;
            else Debug.LogWarning("No Canvas found");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (oilEffectPrefab != null && canvasTransform != null)
        {
            GameObject go = Instantiate(oilEffectPrefab);
            // attach to canvas and keep UI sizing
            go.transform.SetParent(canvasTransform, false);

            OilEffect eff = go.GetComponent<OilEffect>();
            if (eff != null) eff.StartEffect(effectDuration);
        }
    }
}
