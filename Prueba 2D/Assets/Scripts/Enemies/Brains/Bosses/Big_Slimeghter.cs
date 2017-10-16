using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Slimeghter : Basic_Boss {

    public float hopPower = 10;
    public GameObject slimeghterPrefab;
    public GameObject helpPrefab;
    public GameObject[] bossPlatforms;
    public float timeBetweenHelp = 5f;
    private int direction = -1;

    private Basic_Movement movement;
    private CircleCollider2D cc2d;
    private Animator animator;
    private Rigidbody2D rb2d;
    private Renderer myRenderer;
    private bool tookDamage = false;
    private GameObject help;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        print("Component : " + movement);
        cc2d = GetComponent<CircleCollider2D>();
        movement = GetComponent<Basic_Movement>();
        rb2d = GetComponent<Rigidbody2D>();
        myRenderer = GetComponentInChildren<Renderer>();
        deactivatePlatforms();
        pickRoutine(0);
        currentHealth = maxHealth;
    }

    void Update () {
        if (currentHealth<= 0)
        {
            die();
        }
    }

    private void pickRoutine(int routineNumber)
    {
        this.routineNumber = routineNumber;
        Hop_Routine hr;
        switch (routineNumber)
        {
            case 0:
                hr = (Hop_Routine)beginRoutine(0);
                if (currentHealth <= 2)
                {
                    hr.timeBetweenHops = 4f;
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
                if (currentHealth<= 1)
                {
                    hr.timeBetweenHops = 3f;
                    tpa.amount = Mathf.FloorToInt(maxHealth - currentHealth);
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
            if (col.gameObject.tag == "Player")
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
    }

    public void deactivatePlatforms()
    {
        foreach (GameObject g in bossPlatforms)
        {
            g.SetActive(false);
        }
    }

    public override void getStomped()
    {
        
        print(gameObject.name + ", tookDamage? " + tookDamage);
        print(gameObject.name + ", isRecoverying? " + isRecoverying);
        if (!tookDamage && !IsInvoking("summonHelp") && !isRecoverying && help == null)
        {
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

    public void summonHelp()
    {
        CancelInvoke("unsummonHelp");
        if (help == null)
        {
            activatePlatforms();
            for (int i = 0; i < bossPlatforms.Length; i++)
            {
                if (0.5f < Random.value || i == (bossPlatforms.Length - 1))
                {
                    help = Instantiate(helpPrefab);
                    help.transform.position = new Vector2(bossPlatforms[i].transform.position.x, bossPlatforms[i].transform.position.y + 2);
                    break;
                }
            }
            if (!IsInvoking("unsummonHelp"))
                Invoke("unsummonHelp", 6f);
        }
    }

    public void unSummonHelp()
    {
        Destroy(help);
        deactivatePlatforms();
    }
}
