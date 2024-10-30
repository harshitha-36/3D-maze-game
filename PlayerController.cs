using System.Collections;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;        // Player movement speed
    public float rotationSpeed = 100f;  // Speed of rotation around the Y-axis
    public int maxHealth = 5;       // Maximum health of the player
    public int maxLives = 3;        // Maximum lives player can have
    public TextMeshProUGUI healthText;  // UI TMP Text for health
    public TextMeshProUGUI livesText;   // UI TMP Text for lives
    public TextMeshProUGUI statusText;  // UI TMP Text for game status (e.g., You Won, Game Over)

    private int currentHealth;
    private int currentLives;
    private Vector3 spawnPoint;     // Player respawn point

    private void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
        spawnPoint = new Vector3(22.99f, 2.08f, 22.44f); // Set initial spawn position
        UpdateUI();
        statusText.text = "";             // Clear status text at the start
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Move the player forward/backward and sideways with arrow keys (or WASD keys)
        float moveHorizontal = Input.GetAxis("Horizontal");  // Left-Right movement (A/D or Left/Right Arrow)
        float moveVertical = Input.GetAxis("Vertical");      // Forward-Backward movement (W/S or Up/Down Arrow)

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Rotate player around the Y-axis when 'R' key is pressed
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);  // Rotate player around Y-axis
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TakeDamageOverTime());
        }
        else if (other.gameObject.CompareTag("Reward"))
        {
            statusText.text = "You Won!";
        }
    }

    IEnumerator TakeDamageOverTime()
    {
        while (true)
        {
            currentHealth -= 2;
            UpdateUI();

            if (currentHealth <= 0)
            {
                Respawn();
                break;
            }
            yield return new WaitForSeconds(1);  // Damage taken every 1 second
        }
    }

    void Respawn()
    {
        currentLives -= 1;
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            currentHealth = maxHealth;
            transform.position = spawnPoint;
            UpdateUI();
        }
    }

    void GameOver()
    {
        statusText.text = "Game Over!";
        // Add logic here to stop the game
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + currentHealth;
        livesText.text = "Lives: " + currentLives;
    }
}
