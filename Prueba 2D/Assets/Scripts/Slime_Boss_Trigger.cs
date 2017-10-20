using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Boss_Trigger : MonoBehaviour {

    public GameObject closingDoor;
    public GameObject boss;
    

    private bool bossActivated = false;
	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!bossActivated)
        {
            bossActivated = true;
            Move_Speed_Seconds mss = closingDoor.AddComponent<Move_Speed_Seconds>();
            mss.seconds = 3f;
            mss.speed = new Vector2(0, -1f);
            print("Closing door");
            Invoke("activateBoss", 3f);
            SoundManager.instance.fadeOutMusic();
        }
        
    }

    private void activateBoss()
    {
        boss.GetComponent<Basic_Boss>().beginFight();
        Camera.main.GetComponent<camara_movement>().changeSize(6);
        Camera.main.GetComponent<camara_movement>().changeYOffset(0.8f);
        SoundManager.instance.fadeInMusic();
        SoundManager.instance.playMusicByIndex(3);
    }
}
