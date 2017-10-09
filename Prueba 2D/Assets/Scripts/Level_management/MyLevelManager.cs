using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour{

    private AsyncOperation asyncLevelLoad;

    private string loadingLevelName;
    private string previousLevelName ="";
    public bool levelFinishedLoading = false;

    public int getLevelMusicIndexByLevelName(string levelName)
    {
        switch (levelName)
        {
            case "Level_002":
                return 0;
            case "Level_003":
                return 1;
            case "Next_Level_Screen":
                return 2;
            default:
                return 0;
        }
    }

    public string getNextLevelNameFromLevelName(string levelName)
    {
        switch (levelName)
        {
            case "Level_002":
                return "Level_003";
            case "Level_003":
                return "Level_Boss_Slime";
            default:
                return "Level_001";

        }
    }

    public void playCurrentLevelMusic()
    {
        print("Scene right now name = " + SceneManager.GetActiveScene().name);
        SoundManager.instance.playMusicByIndex(getLevelMusicIndexByLevelName(SceneManager.GetActiveScene().name));
    }

    public IEnumerator goToLevelAfterXSeconds(string levelName, float seconds)
    {
        levelFinishedLoading = false;
        asyncLevelLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        asyncLevelLoad.allowSceneActivation = false;
        loadingLevelName = levelName;
        yield return new WaitForSeconds(seconds);
        while (!isLevelInternallyLoaded())
        {
            print("Loading... " + asyncLevelLoad.progress * 100 + "%");
        }
        asyncLevelLoad.allowSceneActivation = true;
        SoundManager.instance.playMusicByIndex(getLevelMusicIndexByLevelName(loadingLevelName));

        levelFinishedLoading = true;
    }

    public IEnumerator goToNextLevelScreen()
    {
        levelFinishedLoading = false;
        asyncLevelLoad = SceneManager.LoadSceneAsync("Next_Level_Screen", LoadSceneMode.Single);
        asyncLevelLoad.allowSceneActivation = false;

        if (SceneManager.GetActiveScene().name != previousLevelName)
            previousLevelName = SceneManager.GetActiveScene().name;
        loadingLevelName = "Next_Level_Screen";

        yield return new WaitForSeconds(5); //Tiempo de fadeOut

        while (!isLevelInternallyLoaded())
        {
            print("Loading... " + asyncLevelLoad.progress * 100 + "%");
        }

        asyncLevelLoad.allowSceneActivation = true;
        SoundManager.instance.playMusicByIndex(getLevelMusicIndexByLevelName(loadingLevelName));
        levelFinishedLoading = true;

    }

    private bool isLevelInternallyLoaded()
    {
        return (asyncLevelLoad.progress > 0.89f);
    }

    public bool isLevelLoaded()
    {
        return levelFinishedLoading;
    }

    public string getPreviousLevelName()
    {
        return previousLevelName;
    }
}
