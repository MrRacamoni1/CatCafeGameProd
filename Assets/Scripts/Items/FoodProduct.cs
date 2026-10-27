using UnityEngine;

public class FoodProduct : MonoBehaviour
{
    [SerializeField] CookingStation station;
    [SerializeField] string productName = "Muffin";
    [SerializeField] TaskStep taskWhenCollected;

    Interactables interactables;

    void Awake()
    {
        interactables = GetComponent<Interactables>();
    }

    public void Collect()
    {
        Inventory.Instance.AddIngredient(productName);
        interactables.HidePrompt();
        gameObject.SetActive(false);
        station.ResetStation();
        TaskManager.Instance.CompleteStep(taskWhenCollected);
    }
}