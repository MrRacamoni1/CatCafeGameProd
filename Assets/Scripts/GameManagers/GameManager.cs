using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    enum GameState { StartScreen, Playing, EndScreen }

    [SerializeField] GameObject startScreenPanel;
    [SerializeField] GameObject endScreenPanel;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCameraPOV playerCameraPOV;

    PlayerControls controls;
    GameState state = GameState.StartScreen;

    void Awake()
    {
        Instance = this;
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
    }

    void Start()
    {
        ShowStartScreen();
    }

    void ShowStartScreen()
    {
        state = GameState.StartScreen;
        startScreenPanel.SetActive(true);
        endScreenPanel.SetActive(false);
        SetPlayerControlEnabled(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void StartGame()
    {
        state = GameState.Playing;
        startScreenPanel.SetActive(false);
        SetPlayerControlEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EndGame()
    {
        state = GameState.EndScreen;
        endScreenPanel.SetActive(true);
        SetPlayerControlEnabled(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SetPlayerControlEnabled(bool isEnabled)
    {
        playerMovement.enabled = isEnabled;
        playerCameraPOV.enabled = isEnabled;
    }

    void Update()
    {
        if (controls.UI.Quit.WasPressedThisFrame())
        {
            Application.Quit();
        }

        if (controls.UI.Restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (state == GameState.StartScreen && controls.UI.Confirm.WasPressedThisFrame())
        {
            StartGame();
        }
        else if (state == GameState.EndScreen)
        {
            if (controls.UI.Confirm.WasPressedThisFrame())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (controls.UI.Quit.WasPressedThisFrame())
            {
                Application.Quit();
            }
        }
    }
}
