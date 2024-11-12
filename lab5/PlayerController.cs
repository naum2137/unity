using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float rotationSpeed = 5500f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Zapobiega skakaniu po wyl¹dowaniu
        }

        // Ruch postaci
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Obracanie postaci
        float mouseX = Input.GetAxis("Mouse X");
        if (mouseX != 0)
        {
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        }

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Zastosowanie grawitacji
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

