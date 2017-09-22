using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore_Health_On_Touch : Health_Restoration {

    private bool wasHealthRestored = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !wasHealthRestored)
        {
            GameMaster.Instance.restorePlayerHealth(healthRestored);
            GetComponent<Spin>().cicleTime /= 4;
            wasHealthRestored = true;
            Invoke("destroySelf", 1f);
        }
    }

    public void destroySelf()
    {
        Destroy(gameObject);
    }
}
