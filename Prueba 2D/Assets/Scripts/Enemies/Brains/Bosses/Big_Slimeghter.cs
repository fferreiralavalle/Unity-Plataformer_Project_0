using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Slimeghter : Basic_Boss {

    public float hopPower = 10;
    public GameObject slimeghterPrefab;
    public GameObject helpPrefab;
    public GameObject[] bossPlatforms;
    public GameObject[] BeeSpawners;
    public float timeBetweenHelp = 5f;
    private int direction = -1;

    private Basic_Movement movement;
    private CircleCollider2D cc2d;
    private Animator animator;
    private Rigidbody2D rb2d;
    private Renderer myRenderer;
    private bool tookDamage = false;
    private GameObject help;
    private bool arePlatformsActive = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        cc2d = GetComponent<CircleCollider2D>();
        movement = GetComponent<Basic_Movement>();
        rb2d = GetComponent<Rigidbody2D>();
        myRenderer = GetComponentInChildren<Renderer>();
        deactivatePlatforms();
        movement.setSpeedToCero();
        movement.enabled = false;
        
    }

    public override void beginFight()
    {
        pickRoutine(0);
        movement.enabled = true;
    }

    void Update () {
        if (currentHealth<= 0)
        {
            foreach (GameObject go in BeeSpawners)
            {
                go.GetComponent<Basic_Spawn>().killAllSpawns();
            }
            Destroy(GameObject.Find(gameObject.name + "-Spawns"));
            die();
        }
    }

    private void pickRoutine(int routineNumber)
    {
        this.routineNumber = routineNumber;
        Hop_Routine hr;
        print(gameObject.name + " - CurrentHealth=" + currentHealth);
        switch (routineNumber)
        {
            case 0:
                hr = (Hop_Routine)beginRoutine(0);
                if (currentHealth <= 2)
                {
                    hr.timeBetweenHops = 3f;
                    summonDoubleBees();
                }
                else
                {
                    hr.timeBetweenHops = 5f;
                }
                break;

            case 1:
                endAllRoutines();
                ThrowPrefabAway tpa = (ThrowPrefabAway)beginRoutine(1);
                tpa.prefab = slimeghterPrefab;
                hr = (Hop_Routine)beginRoutine(0);
                if (currentHealth<= 2)
                {
                    hr.timeBetweenHops = 3f;
                    tpa.amount = Mathf.FloorToInt(maxHealth - currentHealth);
                    summonDoubleBees();
                }
                else
                {
                    hr.timeBetweenHops = 5f;
                    tpa.amount = Mathf.FloorToInt(maxHealth - currentHealth);
                }
                break;

            default: break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDead)
        {
            CircleCollider2D damageCollider = transform.Find("Damage_Collider").GetComponent<CircleCollider2D>();
            if (col.gameObject.tag == "Player" && !tookDamage)
            {
                Debug.Log("Found player");
                if ((transform.position.y + damageCollider.offset.y * 1.5f * transform.localScale.y) < col.transform.position.y)
                {
                    col.gameObject.GetComponent<Player_Controller>().bounceJump();
                    playSoundRandomized(hurtSound);
                    getStomped();
                }
                else if (!isRecoverying)
                {
                    col.gameObject.GetComponent<Player_Controller>().knockBack(transform.position.x);
                    col.gameObject.GetComponent<Player_Controller>().takeDamage(damageDealt);
                }
            }
        }
    }

    public void activatePlatforms()
    {
        foreach (GameObject g in bossPlatforms){
            g.SetActive(true);
        }
        arePlatformsActive = true;
    }

    public void deactivatePlatforms()
    {
        foreach (GameObject g in bossPlatforms)
        {
            g.SetActive(false);
        }
        arePlatformsActive = false;
    }

    public override void getStomped()
    {
        
        print(gameObject.name + ", tookDamage? " + tookDamage);
        print(gameObject.name + ", isRecoverying? " + isRecoverying);
        if (!tookDamage && !IsInvoking("summonHelp") && !isRecoverying && help == null)
        {
            halfSummonHelp();
            Invoke("summonHelp", timeBetweenHelp);
            print("invoking help");
        }
        endRoutine(0);
        animator.SetTrigger("Stomp");
        movement.setAllowMovementToFalse();
        movement.setSpeedToCero();
        setRecoveringToTrue();

        CancelInvoke("beginRecovering");
        CancelInvoke("finishRecovering");
        Invoke("beginRecovering", recoveryTimeInSeconds);
       
    }

    public void beginRecovering()
    {
        animator.SetTrigger("Recover");
        CancelInvoke("finishRecovering");
        Invoke("finishRecovering", animator.GetCurrentAnimatorClipInfo(0).Length);
        playSoundRandomizedWithDelay(recoverySound, 0.15f);
    }

    public void finishRecovering()
    {
        myRenderer.material.color = new Color32(255, 255, 255, 255);
        animator.SetTrigger("Default");
        movement.setAllowMovementToTrue();
        setRecoveringToFalse();
        if (tookDamage)
        {
            pickRoutine(1);
            summonBees();
            minimize();
            tookDamage = false;
        }
        else
        {
            pickRoutine(0);
        }
    }

    public override void takeDamage(float damageDealt)
    {
        currentHealth -= damageDealt;
        tookDamage = true;
        playSoundRandomized(hurtSound);
        myRenderer.material.color = new Color32(0, 0, 0, 255);
        unSummonHelp();
        getStomped();
    }

    public void halfSummonHelp()
    {
        if (!arePlatformsActive)
        {
            activatePlatforms();
            for (int i = 0; i < bossPlatforms.Length; i++)
            {
                Renderer[] renderers = bossPlatforms[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.color = new Color32(255, 255, 255, 30);
                }
                bossPlatforms[i].GetComponent<Collider2D>().enabled = false;

            }
        }
    }

    public void summonHelp()
    {
        CancelInvoke("unSummonHelp");
        if (help == null)
        {
            activatePlatforms();
            for (int i = 0; i < bossPlatforms.Length; i++)
            {
                Renderer[] renderers = bossPlatforms[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.color = new Color32(255, 255, 255, 255);
                }
                bossPlatforms[i].GetComponent<Collider2D>().enabled = true;
                if ((0.5f < Random.value || i != (bossPlatforms.Length - 1)) && help == null)
                {
                    help = Instantiate(helpPrefab);
                    help.transform.position = new Vector2(bossPlatforms[i].transform.position.x, bossPlatforms[i].transform.position.y + 2);
                }

            }
            /* Removed because it was not fun
             * if (!IsInvoking("unSummonHelp"))
                Invoke("unSummonHelp", 8f);*/
        }
    }

    public void unSummonHelp()
    {
        Destroy(help);
        deactivatePlatforms();
    }

    public void minimize()
    {
        if (Mathf.Abs(transform.localScale.x) > 1 && transform.localScale.y > 1)
            transform.localScale = new Vector3(transform.localScale.x - 1 * Mathf.Sign(transform.localScale.x), transform.localScale.y - 1, transform.localScale.z);
    }

    public void summonBees()
    {
        print("Summoning bees");
        for (int i = 0; i<BeeSpawners.Length && i<2; i++)
        {
            Bee_Spawn bs = BeeSpawners[i].GetComponent<Bee_Spawn>();
            if (i == 0)
            {
                bs.spawnPrefabLookingLeft = false;
            }
            bs.Spawn();    
        }
    }

    public void summonDoubleBees()
    {
        summonBees();
        for (int i = 2; i < BeeSpawners.Length && i < 4; i++)
        {
            Bee_Spawn bs = BeeSpawners[i].GetComponent<Bee_Spawn>();
            if (i%2 == 0)
            {
                bs.spawnPrefabLookingLeft = false;
            }
            bs.Spawn();
        }
    }
}
