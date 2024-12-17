using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ImageClickHandler : MonoBehaviour
{
    // Reference to the UI panel that you want to show
    public GameObject uiPanel;

    // Reference for tower selection and player gold management
    public GameManager gameManager; // Reference to the GameManager to handle gold

    // Tower Images (These are just the sprites you want to use for towers)
    public Sprite[] towers; // Sprite for Tower 1

    private Image clickedImage; // Store the clicked image
    public static int clickedIndex; // Store the index of the clicked image
    private bool isPanelOpen = false;
    public int selectedTowerIndex;


    public GameObject[] Guns;
    //private int selectedTowerSpriteIndex;
    //private int currentIndex;

    [SerializeField] private Button[] clickedImages;

    private int[] TowerCost= { 5, 10, 20 };

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (isPanelOpen)
    //    {
    //        return; // Do nothing if the panel is already open
    //    }

    //    // Get the clicked image
    //    clickedImage = GetComponent<Image>();

    //    if (clickedImage != null)
    //    {
    //        // Get the index of the clicked image
    //        clickedIndex = transform.GetSiblingIndex();
    //        Debug.Log("Clicked Image Index: " + clickedIndex);
    //        // Open the panel
    //        StartCoroutine(OpenPanelWithDelay(clickedIndex));
    //    }
    //}

    public void OpenPanel()
    {
        if (isPanelOpen)
        {
            return; // Do nothing if the panel is already open
        }

        //// Get the clicked image
        //clickedImage = GetComponent<Image>();

        //if (clickedImage != null)
        //{
        //    // Get the index of the clicked image
        //    clickedIndex = transform.GetSiblingIndex();
        //    Debug.Log("Clicked Image Index: " + clickedIndex);
        //    // Open the panel
        //}
        StartCoroutine(OpenPanelWithDelay());

    }

    //public void ReturnIndex()
    //{
    //    int index = currentIndex;
    //    //return index;
    //}
    
    private void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // Hide the panel initially
        }
        else
        {
            Debug.LogError("UI Panel not assigned!");
        }
        clickedIndex = 0;
    }

    private IEnumerator OpenPanelWithDelay()
    {
        if (uiPanel != null && !uiPanel.activeSelf)
        {
            uiPanel.SetActive(true); // Show the UI panel with tower options
            PauseGame();
            isPanelOpen = true;
            yield return null;
        }
    }
    public void GetButtonIndex(int index)
    {
        clickedIndex = index;
    }
    private void PauseGame()
    {
        Time.timeScale = 0; // Pauses the game
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Resumes the game
    }

    //public void Method1()
    //{
    //    clickedImage = GetComponent<Image>();

    //    if (clickedImage != null)
    //    {
    //        // Get the index of the clicked image
    //        clickedIndex = transform.GetSiblingIndex();
    //        Debug.Log("Clicked Image Index: " + clickedIndex);
    //        // Open the panel
    //    }
    //}

    // Called when a tower is selected from the panel
    public void OnTowerPanelClicked(int index)
    {
       
        selectedTowerIndex = index;
        print("index " + selectedTowerIndex);
        PlaceTower(selectedTowerIndex, clickedIndex);
    }

    // Replace the clicked image with the selected tower sprite
    public void PlaceTower(int selectedTowerIndex, int clickedIndex)
    {
        if (selectedTowerIndex == -1 || clickedIndex==-1)
        {
            Debug.Log("No tower selected or no image clicked.");
            return;
        }

        // Check if the player has enough gold to place the tower
        int towerCost = TowerCost[clickedIndex]; // Example cost (adjust as needed)
        if (gameManager.DeductGold(towerCost))
        {
            //// Replace the clicked image's sprite based on the selected tower
            //switch (selectedTowerIndex)
            //{
            //    case 0:
            //        clickedImage.sprite = towers[selectedTowerSpriteIndex]; // Replace with Tower 1 sprite
            //        break;
            //    case 1:
            //        clickedImage.sprite = towers[selectedTowerSpriteIndex]; // Replace with Tower 2 sprite
            //        break;
            //    case 2:
            //        clickedImage.sprite = towers[selectedTowerSpriteIndex]; // Replace with Tower 3 sprite
            //        break;
            //}
            clickedImages[clickedIndex].GetComponent<Image>().sprite = towers[selectedTowerIndex];
            Guns[clickedIndex].SetActive(true);
            clickedImages[clickedIndex].GetComponent<SphereCollider>().enabled = true;
            // Close the UI panel after placing the tower
            if (uiPanel != null)
            {
                uiPanel.SetActive(false); // Close the panel
                ResumeGame();
                isPanelOpen = false;
            }


        }
        else
        {
            gameManager.ShowErrorMessage("Not enough gold!");
        }
    }

    // Called when the cancel button is clicked
    public void CancelPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // Close the panel
            ResumeGame();
            isPanelOpen = false;
        }
    }
}
