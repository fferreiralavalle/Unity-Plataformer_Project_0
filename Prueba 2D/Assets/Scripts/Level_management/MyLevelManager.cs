using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour {

    public void goToNextlevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log("Scene = " + activeScene.name);
        GameMaster.Instance.updateInitialCoins();
        switch (activeScene.name)
        {
            case "Level_002":
                SceneManager.LoadScene("Level_003");
                SoundManager.instance.playMusicByIndex(1);
                break ;
            default:
                SceneManager.LoadScene("level_001");
                break;

        }
    }
}
