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
    public GameObject lifePanel;
    public GameObject coinsPanel;
    public GameObject dialogueBox;
    public GameObject nextLevelPanel;

    public float hatHorizontalSeparation = 30;
    public float hatVerticalSeparation = 30;

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
        gameObject.SetActive(true);
    }

    public void updateHealth(float newHealthValue)
    {
        // Destroys old children
        for (int i = 0; i < healthPanel.transform.childCount; i++)
        {
            Destroy(healthPanel.transform.GetChild(i).gameObject);
        }

        //Re creates children
        for (float i =0 ; i < newHealthValue; i++)
        {
            Image lifeHat = Instantiate(imgHat, healthPanel.transform, false);
            Vector2 imgHatSize = imgHat.GetComponent<RectTransform>().sizeDelta;
            Vector2 imgHatScale = imgHat.GetComponent<RectTransform>().localScale;
            lifeHat.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                 hatHorizontalSeparation * (1 + i) + imgHatSize.x * imgHatScale.x * (0.5f + i), 
                -hatVerticalSeparation             - imgHatSize.y * imgHatScale.y / 2);
        }
        
    }

    public void updateMaxHealth(float newMaxHealth)
    {
        Vector2 imgHatScale = imgHat.GetComponent<RectTransform>().localScale;
        Vector2 imgHatSize = imgHat.GetComponent<RectTransform>().sizeDelta;
        healthPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
            imgHatSize.x * imgHatScale.x * newMaxHealth + hatHorizontalSeparation * (2 + newMaxHealth),
            imgHatSize.y * imgHatScale.y                + hatVerticalSeparation   *  2
            );
    }

    public void updateLife(int newLifeValue)
    {
        print("new life is = " + newLifeValue);
        Text number = lifePanel.GetComponentInChildren<Text>();
        int previousCoins = int.Parse(number.text);
        number.text = "" + newLifeValue;
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

    public void showLifes()
    {
        Debug.Log("Showing dialog");
        lifePanel.SetActive(true);
    }

    public void hideLifes()
    {
        Debug.Log("Hiding dialog");
        lifePanel.SetActive(false);
    }

    public void hideAll()
    {
        hidePlayerHealth();
        hideCoins();
        hideLifes();
    }

    public void showAll()
    {
        showPlayerHealth();
        showCoins();
        showLifes();
    }
}
