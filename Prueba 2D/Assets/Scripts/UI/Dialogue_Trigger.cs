using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour {

    public Dialogue[] dialogue;

    public void TriggerDialogue(int indexDialogue)
    {
        Dialogue_Manager.Instance.beginDialogue(dialogue[indexDialogue]);
    }

    public void CloseDialogue() {
        Dialogue_Manager.Instance.endDialogue();
    }
}
