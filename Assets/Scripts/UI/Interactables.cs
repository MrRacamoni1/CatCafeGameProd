using UnityEngine;
using UnityEngine.Events;
using TMPro;

//Commenting out my raycast implementation practice in this script.
public class Interactables : MonoBehaviour
{
    [SerializeField] string promptMessage = "Press E to pick this up";
    [SerializeField] TMP_Text promptText;
    //layerMask objectLayer

    public UnityEvent onInteract;

    private PlayerControls controls;
    private bool playerInRange = false;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptText.text = promptMessage;
            promptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && controls.Player.Interact.WasPressedThisFrame())
        {
            onInteract.Invoke();
        }
    }

    public void SetPromptMessage(string message)
    {
        promptMessage = message;
        
        if (promptText.gameObject.activeSelf)
        {
            promptText.text = message;
        }
    }

    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
        playerInRange = false;
    }
}
