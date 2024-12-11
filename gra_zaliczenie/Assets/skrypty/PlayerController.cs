using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 2f; // Przyspieszenie ruchu
    public float maxSpeed = 5f;     // Maksymalna prędkość w poziomie
    public float airControl = 1f;   // Sterowanie w powietrzu
    public float minJumpForce = 10f; // Minimalna siła skoku
    public float maxJumpForce = 20f; // Maksymalna siła skoku
    public float minJumpAngle = 50f; // Minimalny kąt skoku (przy maksymalnej prędkości)
    public float maxJumpAngle = 90f; // Maksymalny kąt skoku (przy braku prędkości)

    public Transform groundCheck;   // Punkt sprawdzania kontaktu z ziemią
    public float groundCheckRadius = 0.1f; // Promień sprawdzania
    public LayerMask whatIsGround;  // Warstwa reprezentująca ziemię

    public AudioSource audioSource; // Źródło dźwięku
    public AudioClip[] jumpSounds;  // Tablica dźwięków skoku

    private Rigidbody2D rb;
    private bool isGrounded;        // Czy gracz dotyka ziemi
    private float horizontalInput; // Bieżący kierunek ruchu

    private void Start()
    {
        Debug.Log("PlayerController Start() wywołane!");

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Nie znaleziono Rigidbody2D na obiekcie gracza!");
        }

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck nie jest przypisany!");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource nie jest przypisany!");
        }

        // Resetuj prędkość i pozycję
        rb.linearVelocity = Vector2.zero;
        isGrounded = false;
        horizontalInput = 0;

        Debug.Log("Gracz został poprawnie zainicjalizowany.");
    }

    private void Update()
    {
        Debug.Log("PlayerController Update() działa!");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        Debug.Log($"isGrounded: {isGrounded}");

        horizontalInput = Input.GetAxisRaw("Horizontal");
        Debug.Log($"horizontalInput: {horizontalInput}");

        if (Input.GetKey(KeyCode.A)) Debug.Log("Klawisz A wciśnięty!");
        if (Input.GetKey(KeyCode.D)) Debug.Log("Klawisz D wciśnięty!");
        if (Input.GetKey(KeyCode.Space)) Debug.Log("Spacja wciśnięta!");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Skok został wykonany!");
            PerformJump();
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            float targetSpeed = horizontalInput * maxSpeed;
            rb.linearVelocity = new Vector2(
                Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime),
                rb.linearVelocity.y
            );
        }
        else
        {
            float targetAirSpeed = horizontalInput * maxSpeed;
            rb.linearVelocity = new Vector2(
                Mathf.MoveTowards(rb.linearVelocity.x, targetAirSpeed, airControl * Time.fixedDeltaTime),
                rb.linearVelocity.y
            );
        }
    }

    private void PerformJump()
    {
        // Obliczanie bieżącej prędkości w poziomie
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x);

        // Obliczanie kąta skoku na podstawie prędkości
        float normalizedSpeed = currentSpeed / maxSpeed; // Wartość od 0 do 1
        float jumpAngle = Mathf.Lerp(maxJumpAngle, minJumpAngle, normalizedSpeed); // Interpolacja kąta

        // Obliczanie siły skoku na podstawie prędkości
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, normalizedSpeed);

        // Obliczanie składowych prędkości (X i Y) skoku
        float jumpAngleRadians = jumpAngle * Mathf.Deg2Rad; // Kąt w radianach
        float xVelocity = Mathf.Cos(jumpAngleRadians) * jumpForce * Mathf.Sign(rb.linearVelocity.x);
        float yVelocity = Mathf.Sin(jumpAngleRadians) * jumpForce;

        // Ustawienie prędkości skoku
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);

        // Odtwórz losowy dźwięk skoku
        PlayRandomJumpSound();
    }

    private void PlayRandomJumpSound()
    {
        if (jumpSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, jumpSounds.Length); // Wybierz losowy dźwięk
            audioSource.PlayOneShot(jumpSounds[randomIndex]); // Odtwórz dźwięk
        }
    }

    private void OnDrawGizmos()
    {
        // Wizualizacja promienia sprawdzania kontaktu z ziemią
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void ResetPlayer()
    {
        Debug.Log("Resetowanie gracza...");
        rb.linearVelocity = Vector2.zero; // Reset prędkości
        transform.position = new Vector3(0, 2, 0); // Powrót na startową pozycję
        isGrounded = false; // Gracz zaczyna w powietrzu
    }
}
