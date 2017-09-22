using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int coinsGained = 5;
    public AudioClip coinGainedSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameMaster.Instance.gainCoins(coinsGained);
            SoundManager.instance.RandomizeSfx(coinGainedSound);
            Destroy(gameObject);
        }
    }
}
