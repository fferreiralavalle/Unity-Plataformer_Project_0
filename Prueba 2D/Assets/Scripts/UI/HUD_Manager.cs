using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour {

    public static HUD_Manager Instance { get; set; }

    public Image imgHat;
    public Image backgroundHats;
    public GameObject gameOverPanel;
    public GameObject healthPanel;
    public GameObject dialogueBox;
    public GameObject winLevelPanel;

    public float hatSize = 1;
    public float hatVerticalSeparation = 30;
    public float hatHorizontalSeparation = 30;

    private RectTransform rectTrans;

    void Awake () {
        if (Instance == null)
        {
            Instance = this;
            rectTrans = GetComponent<RectTransform>();

            backgroundHats = Instantiate(backgroundHats, healthPanel.transform, false);

            dialogueBox.SetActive(false);

            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this){
            Destroy(gameObject);
        }
        
        
    }

    public void updateLifes(float newLifeValue)
    {
        // Destroys old children
        for (int i = 0; i < healthPanel.transform.childCount; i++)
        {
            Destroy(healthPanel.transform.GetChild(i).gameObject);
        }

        //Re creates children
        for (float i =0 ; i < newLifeValue; i++)
        {
            Image lifeHat = Instantiate(imgHat, healthPanel.transform, false);
            lifeHat.GetComponent<RectTransform>().anchoredPosition = new Vector2( hatVerticalSeparation *(0.5f + i), -hatHorizontalSeparation);
            
        } 
    }

    public void showGameOverPanel()
    {
        Debug.Log("Creating game over screen...");
        GameObject gameOverPanelShown = Instantiate(gameOverPanel, transform, false);
    }

    public void showWinLevelPanel()
    {
        Debug.Log("Creating game over screen...");
        GameObject gameOverPanelShown = Instantiate(winLevelPanel, transform, false);
    }

    public void showDialogueBox()
    {
        Debug.Log("Showing dialog");
        dialogueBox.SetActive(true);
    }

    public void hideDialogueBox()
    {
        Debug.Log("Hiding dialog");
        dialogueBox.SetActive(false);
    }
}
/*Next level YEAAAHAHHHAH!

(There's no next level actually I'm sorry man) 
(DONT PRESS ME!)
*/