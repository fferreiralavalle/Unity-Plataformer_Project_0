using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour {

    public void goToNextlevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log("Scene = " + activeScene.name);
        //GameMaster.Instance.getPlayer().transform.position = new Vector3(0, 5);
        print("PROBLEM MAY BE HERE");
        GameMaster.Instance.updateInitialCoins();
        switch (activeScene.name)
        {
            case "Level_002":
                SceneManager.LoadScene("Level_003");
                break ;
            default:
                SceneManager.LoadScene("level_001");
                break;

        }
    }
}
