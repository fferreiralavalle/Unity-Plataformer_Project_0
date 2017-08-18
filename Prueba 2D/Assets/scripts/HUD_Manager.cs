using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour {

    public static HUD_Manager Instance { get; set; }

    public Image imgHat;
    public Image backgroundHats;
    public float hatSize = 1;
    public float hatVerticalSeparation = 30;
    public float hatHorizontalSeparation = 30;

    private RectTransform rectTrans;
	void Awake () {
        Instance = this;
        rectTrans = GetComponent<RectTransform>();

        backgroundHats = Instantiate(backgroundHats, transform, false);
    }
	
	
	void Update () {
		
	}

    public void updateLifes(float newLifeValue)
    {
        Debug.Log("updating... "+ newLifeValue + " lifes!");
        // Destroys old children
        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //Re creates children
        for (float i =0 ; i < newLifeValue; i++)
        {
            Debug.Log("index = "+i);
            Image lifeHat = Instantiate(imgHat, transform, false);
            lifeHat.GetComponent<RectTransform>().anchoredPosition = new Vector2( hatVerticalSeparation *(0.5f + i), -hatHorizontalSeparation);
            
        } 
    }
}
