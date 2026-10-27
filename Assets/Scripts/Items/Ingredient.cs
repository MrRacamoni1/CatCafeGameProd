using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] string ingredientName = "Insert ingredient name";
    Interactables interactables;

    void Awake()
    {
        interactables = GetComponent<Interactables>();
    }

    public void Pickup()
    {
        Inventory.Instance.AddIngredient(ingredientName);
        interactables.HidePrompt();
        gameObject.SetActive(false);
    }
}