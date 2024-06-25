using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditor.Rendering;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogParant;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject PlayerUi;
    [SerializeField] private Button ButtonOption1;
    [SerializeField] private Button ButtonOption2;

    [SerializeField] private float TypingSpeed = 0.05f;
    [SerializeField] private float TurnSpeed = 2f;

    private List<dialogString> dialogList;

    [Header("Player")]

    [SerializeField] private Player Player;

    private int CurtentDialogIndex = 0;

    private void Start()
    {   // pakt de dialog text vak en de buttons en zet ze op fales zodra het spel begint
        dialogParant.SetActive(false);
        ButtonOption1.gameObject.SetActive(false);
        ButtonOption2.gameObject.SetActive(false);
    }

    public void DailogStart(List<dialogString> textToPrint, Transform NPC) 
    {
        // wanneer dit gebeurt word de dialog ui Objects op true gezet  en de player op false
        dialogParant.SetActive(true);
        ButtonOption1.gameObject.SetActive(true);
        ButtonOption2.gameObject.SetActive(true);
        Player.enabled = false;

        //onthoud de dialoglist en print ze naar de text
        dialogList = textToPrint;
        //set de huidege dialog index op 0 zo dat het daar altijd begint 
        CurtentDialogIndex = 0;

        // de 2 funties worden op deze manier aangeroepen zo dat de player eerst moet wacten tot dat de
        // dialog voorbij is en dan pas kan clicken op een button  
        DisableButtons();

        StartCoroutine(printDialog());
    }

    private void DisableButtons()
    {
        // deze funtie zet de buttons op false en laat de text NO OPTION zien op de buttons text compnent
        ButtonOption1.interactable = false;
        ButtonOption2.interactable = false;

        ButtonOption1.GetComponentInChildren<TMP_Text>().text = "NO OPTION";
        ButtonOption2.GetComponentInChildren<TMP_Text>().text = "NO OPTION";

    }
    private bool optionSelected = false;
    // doet alle dialog gerelateerde dingen 
    private IEnumerator printDialog()
    {//zorgt er voor dat alle dialog op de juiste volgorde word laten zien 
        while(CurtentDialogIndex < dialogList.Count)
        {// pakt de juiste line uit de dialoglist
            dialogString line = dialogList[CurtentDialogIndex];
            // dit laat het begin van een dialog event zien 
            line.StartDialogEvent?.Invoke();
            
            // dit is een if statement die kijkt of de line een vraag is in dat gevall pakt
            // hij de buttons en zorgt er voor dat je er op kan clicken
            if (line.IsQuestion) 
            {
                yield return StartCoroutine(TypeText(line.text));

                ButtonOption1.interactable = true;
                ButtonOption2.interactable = true;

                ButtonOption1.GetComponentInChildren<TMP_Text>().text = line.answerQuestion1;
                ButtonOption2.GetComponentInChildren<TMP_Text>().text = line.answerQuestion2;

                ButtonOption1.onClick.AddListener(() => handleOptionSelected(line.Option1IndexJump));
                ButtonOption2.onClick.AddListener(() => handleOptionSelected(line.Option2IndexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {// en anders laat de dialog line gewoon zien 
                yield return StartCoroutine(TypeText(line.text));
            }
            // wordt aangeroepen als de dialog klaar is 
            line.EndDialogEvent?.Invoke();
            // hier gaat hij naar de volgende line en zet hij de optionSelected weer op false zo dat je weer
            // een knop kan kiezen 
            CurtentDialogIndex++;
            optionSelected = false;
        }
        // als de klaar is word de dialogstop gezet 
        DialogStop();
    }

    private void handleOptionSelected(int indexJump) 
    {// laat de speler springen naar een bepalde dialog index gebaserd op een knop keuze
        Debug.Log("Option selected with index: " + indexJump);
        optionSelected = true;
        DisableButtons();

        CurtentDialogIndex = indexJump;

    }

    private IEnumerator TypeText(string text)
    {// set de dialog leeg en tiept de zin van de dialouge leter voor letter en als dat gebeurt word die op
     // stop gezet
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        if (!dialogList[CurtentDialogIndex].IsQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogList[CurtentDialogIndex].IsEnd)
            DialogStop();
    }

    private void DialogStop()
    {// stopt alle dialogue gerelateerde dingen garandeert dat de player weer aangezet is en de muis
     // gezien kan worden 
        StopAllCoroutines();
        dialogueText.text = "";
        dialogParant.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        ButtonOption1.gameObject.SetActive(false);
        ButtonOption2.gameObject.SetActive(false);

        Player.enabled = true;

        
        Cursor.visible = true;

    }
}
