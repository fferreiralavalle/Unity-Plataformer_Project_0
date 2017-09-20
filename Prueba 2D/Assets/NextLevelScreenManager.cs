using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelScreenManager : MonoBehaviour {

    public static NextLevelScreenManager instance { get; set; }

    public GameObject levelManagerPrefab;

    private string nextLevelName = "Level_002";
    
    private MyLevelManager levelManager;

    void Awake () {
        if (instance == null)
        {
            instance = this;
            //Creates a levelManager so it can use the invoke function and others.
            levelManagerPrefab = Instantiate(levelManagerPrefab, null, false);
            levelManager = levelManagerPrefab.GetComponent<MyLevelManager>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SoundManager.instance.playMusicByIndex(2);
    }
	
    public void setNextLevelName(string name)
    {
        nextLevelName = name;
    }

    public void goToNextLevel()
    {
        levelManager.goToLevelAfterXSeconds(nextLevelName, gameObject.GetComponent<Fading>().beginFade(1));
        
    }
}
