using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive_Player_From_UI : MonoBehaviour {

    public void revivePlayer()
    {
        GameMaster.Instance.resetLevel();
        GameMaster.Instance.revivePlayer();
        GameMaster.Instance.goToIntialSpawn();
        Destroy(gameObject);
    }
}
