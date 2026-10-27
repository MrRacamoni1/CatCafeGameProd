using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    Dictionary<string, int> ingredientCounts = new Dictionary<string, int>();

    void Awake()
    {
        Instance = this;
    }

    public void AddIngredient(string ingredientName)
    {
        if (ingredientCounts.ContainsKey(ingredientName))
        {
            ingredientCounts[ingredientName]++;
        }
        else
        {
            ingredientCounts[ingredientName] = 1;
        }
    }

    public void RemoveIngredient(string ingredientName)
    {
        if (ingredientCounts.ContainsKey(ingredientName))
        {
            ingredientCounts[ingredientName]--;
        }
    }

    public int GetCount(string ingredientName)
    {
        if (ingredientCounts.ContainsKey(ingredientName))
        {
            return ingredientCounts[ingredientName];
        }
        return 0;
    }

    public IEnumerable<KeyValuePair<string, int>> GetAllItems()
    {
        return ingredientCounts;
    }
}