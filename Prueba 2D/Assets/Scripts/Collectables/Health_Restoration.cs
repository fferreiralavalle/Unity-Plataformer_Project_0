using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Restoration : MonoBehaviour {

    public float healthRestored = 1f;

	public void restoreHealthToPlayer()
    {
        GameMaster.Instance.restorePlayerHealth(healthRestored);
    }
}
