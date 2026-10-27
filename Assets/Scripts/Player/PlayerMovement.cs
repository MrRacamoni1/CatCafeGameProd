using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float gravity = -9.81f;

    CharacterController charctrl;
    PlayerControls controls;
    float verticalVelocity = 0f;

    void Awake()
    {
        charctrl = GetComponent<CharacterController>();
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

    void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();

        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        charctrl.Move(moveDirection * moveSpeed * Time.deltaTime);

        verticalVelocity += gravity * Time.deltaTime;
        charctrl.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);

        if (charctrl.isGrounded)
        {
            verticalVelocity = 0f;
        }
    }
}
