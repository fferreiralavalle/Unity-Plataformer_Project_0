using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Spawn : Basic_Spawn {

    public bool spawnPrefabLookingLeft = true;
    public GameObject mover;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override void afterSpawn(GameObject go)
    {
        GameObject bc = Instantiate(mover);
        go.transform.parent = bc.transform;

        Bee bee = go.GetComponent<Bee>();
        Horizontal_Line hl = go.GetComponent<Horizontal_Line>();
        print(gameObject.name + ", spawnPrefabLookingLeft=" + spawnPrefabLookingLeft);
        if (!spawnPrefabLookingLeft)
        {
            hl.forceTurnBack();
        }
            

        Move_Speed_Seconds mss = bc.AddComponent<Move_Speed_Seconds>();
        mss.speed = new Vector2(0, -0.5f);
    }
    



}
