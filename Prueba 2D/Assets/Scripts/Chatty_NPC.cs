using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatty_NPC : MonoBehaviour
{
    public GameObject chattyBubble;

    private BoxCollider2D boxCollider;
    private Dialogue_Trigger dialogueTrigger;
    private Animator animator;

    private GameObject playerCollided;
    private GameObject myChattyBubble;
    private bool isCloseToPlayer = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        dialogueTrigger = GetComponent<Dialogue_Trigger>();
        myChattyBubble = Instantiate(chattyBubble, transform, false);
        myChattyBubble.transform.localPosition = boxCollider.size;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Accept") && Dialogue_Manager.Instance.isFree() && isCloseToPlayer)
        {
            dialogueTrigger.TriggerDialogue(0);
        }
        if (playerCollided != null)
        {
            float direction = Mathf.Sign(playerCollided.transform.position.x - transform.position.x);
            transform.localScale = new Vector3(direction, transform.localScale.y,1 );
            myChattyBubble.transform.localScale = new Vector3(direction, transform.localScale.y, 1);
            myChattyBubble.transform.localPosition = new Vector3(boxCollider.size.x*direction, boxCollider.size.y, 1);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Chatting collision! :D");
            playerCollided = collision.gameObject;
            isCloseToPlayer = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueTrigger.CloseDialogue();
        animator.SetBool("Talking", false);
        isCloseToPlayer = false;
    }
}
