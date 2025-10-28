using UnityEngine;
using TMPro;

public class ItemCounterLabel : MonoBehaviour
{
    public TMP_Text label;

    void Start()
    {
        SaveSystem.Load();
        if (label) label.text = $"Total Items: {SaveSystem.Data.totalItems}";
    }
}
