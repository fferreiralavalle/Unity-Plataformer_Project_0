using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_simple_Spawn_Controller : MonoBehaviour {

    public GameObject enemy;                // The enemy prefab to be spawned.
    public AudioSource efxSource;           //Spawning sound
    public float spawnTime = 4f;            // How long between each spawn.
    //public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

    private AudioSource audioSource;
    private bool isDisabled = false;

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        InvokeRepeating("spawnAnimation", spawnTime -1, spawnTime);
        audioSource = GetComponent<AudioSource>();
    }

    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        if (!isDisabled)
        {
            GameObject newEnemy = Instantiate(enemy, transform);
            newEnemy.transform.Rotate(Vector3.forward, Mathf.PI);
        }
        
    }
    void spawnAnimation()
    {
        if (!isDisabled)
        {
            GameObject tuerca = transform.GetChild(0).gameObject;
            tuerca.GetComponent<Animator>().SetTrigger("Rotate");
            audioSource.PlayDelayed(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isDisabled = true;
        }
    }
}
