using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BagProgressBar : MonoBehaviour
{
    [Header("UI References")]
    public Slider bagSlider;
    public TMP_Text bagText;

    [Header("Goal Settings")]
    public int requiredBags = 12;

    private int currentBags = 0;

    void Start()
    {
        // Load saved total from ItemBank if available
        if (ItemBank.Instance != null)
        {
            currentBags = ItemBank.Instance.TotalItems % requiredBags;
        }

        bagSlider.maxValue = requiredBags;
        bagSlider.value = currentBags;
        UpdateText();
    }

    public void AddBag()
    {
        currentBags++;
        bagSlider.value = Mathf.Min(currentBags, requiredBags);
        UpdateText();
    }

    private void UpdateText()
    {
        if (bagText != null)
        {
            bagText.text = $"Bags: {currentBags} / {requiredBags}";
        }
    }
}
