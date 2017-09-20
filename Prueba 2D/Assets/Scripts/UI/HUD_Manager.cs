using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour {

    public static HUD_Manager Instance { get; set; }

    public Image imgHat;
    public Image imgCoin;
    public GameObject gameOverPanel;
    public GameObject healthPanel;
    public GameObject coinsPanel;
    public GameObject dialogueBox;
    public GameObject nextLevelPanel;

    public float hatSize = 1;
    public float hatVerticalSeparation = 30;
    public float hatHorizontalSeparation = 30;

    private RectTransform rectTrans;

    void Awake () {
        if (Instance == null)
        {
            Instance = this;
            rectTrans = GetComponent<RectTransform>();
            dialogueBox.SetActive(false);
            nextLevelPanel.SetActive(false);
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

    public void updateCoins(int newCoins)
    {
        Text number = coinsPanel.GetComponentInChildren<Text>();
        int previousCoins = int.Parse(number.text);
        number.text = ""+newCoins;
    }

    public void showGameOverPanel()
    {
        Debug.Log("Creating game over screen...");
        GameObject gameOverPanelShown = Instantiate(gameOverPanel, transform, false);
    }

    public void showNextLevelPanel(float coins)
    {
        Debug.Log("Creating game over screen...");
        Text coinAmount = nextLevelPanel.transform.Find("Stat_Coins").Find("Coins_Collected").GetComponent<Text>();
        coinAmount.text = ""+coins;
        nextLevelPanel.SetActive(true);
    }

    public void hideNextLevelPanel()
    {
        nextLevelPanel.SetActive(false);
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

    public void showPlayerHealth()
    {
        Debug.Log("Showing player health");
        healthPanel.SetActive(true);
    }

    public void hidePlayerHealth()
    {
        Debug.Log("Hiding player health");
        healthPanel.SetActive(false);
    }

    public void showCoins()
    {
        Debug.Log("Showing dialog");
        coinsPanel.SetActive(true);
    }

    public void hideCoins()
    {
        Debug.Log("Hiding dialog");
        coinsPanel.SetActive(false);
    }

    public float fadeIn()
    {
        return GetComponent<Fading>().beginFade(-1);
    }

    public float fadeOut()
    {
        return GetComponent<Fading>().beginFade(1);
    }
}
