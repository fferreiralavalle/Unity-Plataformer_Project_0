using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBeginNextLevel : MonoBehaviour {

	public void beginNextLevel()
    {
        GameMaster.Instance.beginNextLevel();
    }
}
