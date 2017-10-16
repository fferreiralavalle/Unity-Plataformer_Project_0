using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabOnTouch : MonoBehaviour {

    public GameObject prefab;
    public Vector2 offsets = Vector2.zero;
    public bool destroyAfterFirstSpawn = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = new Vector2(transform.position.x + offsets.x, transform.position.y + offsets.y);
            if (destroyAfterFirstSpawn)
                Destroy(gameObject);
        }
        
    }
}
