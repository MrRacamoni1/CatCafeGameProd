using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text inventoryText;

    void Update()
    {
        string display = "Carrying:\n";
        bool carryingAnything = false;

        foreach (KeyValuePair<string, int> item in Inventory.Instance.GetAllItems())
        {
            if (item.Value > 0)
            {
                display += item.Key + " x" + item.Value + "\n";
                carryingAnything = true;
            }
        }

        if (!carryingAnything)
        {
            display += "Nothing\n";
        }

        inventoryText.text = display;
    }
}