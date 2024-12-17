using UnityEngine;
using TMPro;  // For TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public int playerGold = 100;  // Player's initial gold

    public int playerHealth = 100;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;  // The UI text to show the current gold
    //public TextMeshProUGUI errorText; // The UI text for displaying errors (e.g., "Not enough gold")

    [SerializeField] private GameObject endPanel;

    private void Start()
    {
        UpdateGoldText(); // Initialize the gold text when the game starts
        //errorText.gameObject.SetActive(false); // Hide the error text initially
    }

    // Method to deduct gold when a tower is placed
    public bool DeductGold(int amount)
    {
        if (playerGold > amount)
        {
            playerGold -= amount; // Deduct gold if the player has enough
            UpdateGoldText();
            return true; // Successfully deducted gold
        }
        else
        {
            ShowErrorMessage("Not enough gold!");
            return false; // Not enough gold
        }
    }

    public bool IncreaseGold(int amount)
    {
        
            playerGold += amount; 
            UpdateGoldText();
            return true;
    }

    // Updates the gold UI text
    private void UpdateGoldText()
    {
        goldText.text = playerGold.ToString();
    }

    public bool DeductHealth(int amount)
    {
        if (playerHealth > amount)
        {
            playerHealth -= amount; // Deduct gold if the player has enough
            UpdateHealthText();
            return true; // Successfully deducted gold
        }
        else
        {
            endPanel.SetActive(true);
            ShowErrorMessage("Not enough Health!");
            return false; // Not enough Health
        }
    }

    // Updates the gold UI text
    private void UpdateHealthText()
    {
        healthText.text = playerHealth.ToString();
    }


    // Displays an error message if the player has insufficient gold
    public void ShowErrorMessage(string message)
    {
        //errorText.text = message;
        //errorText.gameObject.SetActive(true); // Show the error message
    }

    // Hides the error message
    public void HideErrorMessage()
    {
        //errorText.gameObject.SetActive(false);
    }
}
