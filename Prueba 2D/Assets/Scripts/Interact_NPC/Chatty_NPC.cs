using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatty_NPC : MonoBehaviour
{
    public GameObject chattyBubble;
    public bool looksAtPlayer = false;

    private Vector3 chattyBubblePosition = Vector3.zero;
    private Dialogue_Trigger dialogueTrigger;
    private Animator animator;

    private GameObject playerCollided;
    private GameObject myChattyBubble;
    private bool isCloseToPlayer = false;
    private int npcInitialDirection;

    void Start()
    {
       
        dialogueTrigger = GetComponent<Dialogue_Trigger>();
        myChattyBubble = Instantiate(chattyBubble, null, false);

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            chattyBubblePosition = boxCollider.size;
        }
        else
        {
            CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
            if (circleCollider != null){
                chattyBubblePosition = new Vector3(circleCollider.bounds.size.x, circleCollider.bounds.size.y);
            }
        }

        myChattyBubble.transform.position = transform.position + chattyBubblePosition;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Accept") && Dialogue_Manager.Instance.isFree() && isCloseToPlayer)
        {
            dialogueTrigger.TriggerDialogue(0);
        }
        if (playerCollided != null && looksAtPlayer)
        {
            float direction = Mathf.Sign(playerCollided.transform.position.x - transform.position.x);
            transform.localScale = new Vector3(direction, transform.localScale.y,1 );
        }
        //updates bubble position
        myChattyBubble.transform.position = transform.position + chattyBubblePosition;

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
        if (collision.tag == "Player")
        {
            dialogueTrigger.CloseDialogue();
            if (animator != null) {
                animator.SetBool("Talking", false);
            }
            isCloseToPlayer = false;
        }
    }
}
