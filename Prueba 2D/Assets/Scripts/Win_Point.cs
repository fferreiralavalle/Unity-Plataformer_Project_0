using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Point : MonoBehaviour {

    public AudioClip winSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            SoundManager.instance.PlaySingle(winSound);
            SoundManager.instance.pauseMusic();
            GetComponent<Spin>().cicleTime /= 4;
           GameMaster.Instance.goToNextLevelScreen();
        }
    }
}
