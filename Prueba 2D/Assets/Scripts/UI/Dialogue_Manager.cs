using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour {

    public static Dialogue_Manager Instance { get; set; }

    public Text nameText;
    public Text dialogueText;
    public float charactersPerFrame = 0.75f;

    private Queue<string> sentences;
    private float currentDisplayingCharacters = 0;
    private string currentSentence = "";
    private AudioClip currentVoice;
    private bool isDialogueFree = true;
    private bool wasDialogueFreePreviousFrame = false;

    private bool isDialoguePaused = false;
    public float pausePerDotInSeconds = 1;
    public float pausePerComaInSeconds = 0.5f;

    void Start () {
        
        if (Instance == null)
        {
            Instance = this;
            sentences = new Queue<string>();
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this){
            Destroy(gameObject);
        }
        
    }
    
    public void beginDialogue(Dialogue dialogue)
    {
        Debug.Log("Now chating with " + dialogue.name);

        HUD_Manager.Instance.showDialogueBox();

        nameText.text = dialogue.name;
        currentVoice = dialogue.voiceSound;
        dialogueText.text = "";
        isDialogueFree = false;
        sentences.Clear();

        foreach(string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence);
        }

        displayNextSentence();
    }

    public void displayNextSentence()
    {

        if (currentDisplayingCharacters < currentSentence.Length)
        {
            currentDisplayingCharacters = currentSentence.Length - 1;
            print("text advanced");
        }
        else if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }
        else
        {
            currentDisplayingCharacters = 0;
            currentSentence = sentences.Dequeue();
        }
        Debug.Log("Sentence : " + currentSentence);
    }

    public void endDialogue()
    {
        sentences.Clear();
        currentSentence = "";
        HUD_Manager.Instance.hideDialogueBox();
        SoundManager.instance.stopVoice();
        Debug.Log("Ending... dialog");
        Invoke("setConversationToFinish", 0.5f);
    }

    public void setConversationToFinish()
    {
        isDialogueFree = true;
        wasDialogueFreePreviousFrame = false;
    }

    void FixedUpdate()
    {
        if (currentDisplayingCharacters < currentSentence.Length && !isDialoguePaused)
        {
            string character = currentSentence.Substring((int)currentDisplayingCharacters, 1);

            if (character.Equals(".") || character.Equals("!") || character.Equals("?"))
            {
                pauseDialogueForXSeconds(pausePerDotInSeconds);
            }
            else if (character.Equals(","))
            {
                pauseDialogueForXSeconds(pausePerComaInSeconds);
            }
            else
            {
                SoundManager.instance.RandomizeVoice(currentVoice);
            }

            dialogueText.text = currentSentence.Substring(0, (int)currentDisplayingCharacters + 1);

            currentDisplayingCharacters += charactersPerFrame;
        }
    }

    private void Update()
    {
        //Checkea que se apriete el boton para continuar y que haya una conversacion.
        
        if (Input.GetButtonDown("Accept") && !wasDialogueFreePreviousFrame)
        {
            displayNextSentence();
        }

        wasDialogueFreePreviousFrame = isDialogueFree;
    }

    public bool isFree()
    {
        Debug.Log("Is chat free ? " + isDialogueFree);

        return isDialogueFree;
    }

    public void pauseDialogueForXSeconds(float seconds)
    {
        isDialoguePaused = true;
        Invoke("unPauseDialogue", seconds);
    }

    public void unPauseDialogue()
    {
        isDialoguePaused = false;
    }
}
