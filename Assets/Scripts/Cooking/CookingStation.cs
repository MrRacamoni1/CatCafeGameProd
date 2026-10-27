using UnityEngine;

public class CookingStation : MonoBehaviour
{
    enum StationState { Empty, Loaded, Cooking, Ready }

    [SerializeField] string[] requiredIngredients = { "Flour", "Egg", "Milk" };
    [SerializeField] float cookDuration = 10f;

    [SerializeField] Interactables interactables;
    [SerializeField] Collider stationCollider;
    [SerializeField] GameObject productObject;

    [Header("Task list (None = report nothing)")]
    [Tooltip("Ticked when the player is carrying everything this station needs.")]
    [SerializeField] TaskStep taskWhenGathered;
    [Tooltip("Ticked when the ingredients are placed into the station.")]
    [SerializeField] TaskStep taskWhenLoaded;

    StationState state = StationState.Empty;
    float cookTimer = 0f;
    bool gatheredReported = false;

    public void Interact()
    {
        switch (state)
        {
            case StationState.Empty:
                TryLoadIngredients();
                break;

            case StationState.Loaded:
                StartCooking();
                break;

            case StationState.Cooking:
                interactables.SetPromptMessage("Still cooking...");
                break;
        }
    }

    bool HasAllIngredients()
    {
        if (requiredIngredients.Length == 0) return false;

        foreach (string ingredient in requiredIngredients)
        {
            if (Inventory.Instance.GetCount(ingredient) <= 0)
            {
                return false;
            }
        }
        return true;
    }

    void TryLoadIngredients()
    {
        if (HasAllIngredients())
        {
            foreach (string ingredient in requiredIngredients)
            {
                Inventory.Instance.RemoveIngredient(ingredient);
            }

            state = StationState.Loaded;
            interactables.SetPromptMessage("Press E to start cooking");
            TaskManager.Instance.CompleteStep(taskWhenLoaded);
        }
        else
        {
            interactables.SetPromptMessage("Missing ingredients");
        }
    }

    void StartCooking()
    {
        state = StationState.Cooking;
        cookTimer = 0f;
        interactables.SetPromptMessage("Cooking...");
    }

    void Update()
    {
        if (state == StationState.Empty && !gatheredReported && HasAllIngredients())
        {
            gatheredReported = true;
            TaskManager.Instance.CompleteStep(taskWhenGathered);
        }

        if (state == StationState.Cooking)
        {
            cookTimer += Time.deltaTime;

            if (cookTimer >= cookDuration)
            {
                FinishCooking();
            }
        }
    }

    void FinishCooking()
    {
        state = StationState.Ready;
        productObject.SetActive(true);
        stationCollider.enabled = false;
        interactables.HidePrompt();
    }

    public void ResetStation()
    {
        state = StationState.Empty;
        stationCollider.enabled = true;
        interactables.SetPromptMessage("Press E to interact");
    }
}