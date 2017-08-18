using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

	public int CheckPointPriority = 1;
    public Sprite flagReachedSprite;

    private SpriteRenderer sr;
    private AudioSource audioS;

    void Awake () {
        sr = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameMaster.Instance.updateSpawn(CheckPointPriority, transform.position);
            if (sr.sprite != flagReachedSprite)
            {
                audioS.Play();
                sr.sprite = flagReachedSprite;
            }
            
            
            
        }
    }
}
