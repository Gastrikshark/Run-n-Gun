using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{// pakt de dialogstring script en maakt en er een newe object van 
    [SerializeField] private List<dialogString> dialogueStrings = new List<dialogString>();
 //  en roept hier de NPC aan 
    [SerializeField] private Transform NPCTransform;
    // kijkt of er all met de npc gesproken is deze staat standart op false
    private bool hasSpoken = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpoken)
        {// als de object met deze script met de player interact pakt hij de dialogManager van de player
         // en print de lines die je in de editor hebt gemaakt hij zet daarna ook has spoken op true zodat
         // je niet in een eindelose loop vast zit
            other.gameObject.GetComponent<DialogManager>().DailogStart(dialogueStrings, NPCTransform);
            hasSpoken = true;
        }
    }
}


[System.Serializable]
public class dialogString
{// pakt alle objecten die bij de dialog horen zodat de player ze self kan uitkiezen
    public string text; 
    public bool IsEnd; 

    [Header("Branch")]
    public bool IsQuestion;
    public string answerQuestion1;
    public string answerQuestion2;
    public int Option1IndexJump;
    public int Option2IndexJump;

    [Header("Triggerd effents")]
    public UnityEvent StartDialogEvent;
    public UnityEvent EndDialogEvent;

}
