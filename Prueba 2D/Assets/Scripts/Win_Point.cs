using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Point : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            HUD_Manager.Instance.showWinLevelPanel();
        }
    }
}
