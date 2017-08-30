using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSelf : MonoBehaviour {

	public void closeSelf()
    {
        Destroy(gameObject);
    }

    public void closeSelf(float seconds)
    {
       Invoke("selfDestroy",seconds);
    }

    public void selfDestroy()
    {
        Destroy(gameObject);
    }
}
