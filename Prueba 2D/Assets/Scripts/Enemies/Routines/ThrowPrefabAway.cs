using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPrefabAway : MonoBehaviour
{

    public int amount = 2;
    public Vector2 force = new Vector2(2 , 5);
    public GameObject prefab;

    private Animator anim;
    private GameObject spawns;

    void Start()
    {
        Invoke("throwSlimeghter",0.1f);
        spawns = GameObject.Find(gameObject.name + "-Spawns");
        if (spawns == null)
            spawns = new GameObject(gameObject.name + "-Spawns");
    }

    public void throwSlimeghter()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Instantiate(prefab, spawns.transform);
            instance.transform.position = transform.position;
            float xForce = force.x * Mathf.Cos( ((float)i) / (amount - 1) * Mathf.PI);
            Vector2 rotatedForce = new Vector2(xForce, force.y);
            Horizontal_Line_Speed hls = instance.GetComponent<Horizontal_Line_Speed>();
            if (hls != null)
            {
                hls.disableAndResetWithDealy(2f);
            }
                
            instance.GetComponent<Rigidbody2D>().AddForce(rotatedForce, ForceMode2D.Impulse);
            
        }
        //Destroys component so it can be called again
        Destroy(this);
    }
}
