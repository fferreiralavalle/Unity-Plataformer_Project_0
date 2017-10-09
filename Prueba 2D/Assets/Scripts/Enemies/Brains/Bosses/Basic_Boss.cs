using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Basic_Boss : Basic_Enemy {

    public string[] routines;
    protected int routineNumber = 0;
    protected bool hasFightBegun = false;
    protected List<int> activeRountineIndexes = new List<int>();

    void Update () {
		
	}

    public void beginFight()
    {
        hasFightBegun = true;
    }

    public void beginRoutine(int routineNumber) {
        if (gameObject.GetComponent(Type.GetType(routines[routineNumber])) == null)
        {
            gameObject.AddComponent( Type.GetType(routines[routineNumber]) );
            activeRountineIndexes.Add(routineNumber);
        }
    }

    public void endRoutine(int routineNumber)
    {
        if (gameObject.GetComponent(Type.GetType(routines[routineNumber])) != null)
        {
            Destroy(GetComponent(Type.GetType(routines[routineNumber])));
        }
    }

    public void endAllRoutines()
    {
        foreach (int index in activeRountineIndexes)
        {
            Destroy(GetComponent(Type.GetType(routines[index])));
        }
    }
}
