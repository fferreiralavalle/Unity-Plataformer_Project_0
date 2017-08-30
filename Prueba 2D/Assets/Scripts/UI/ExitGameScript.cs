using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour {
	
    public void exitGame() {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

}
