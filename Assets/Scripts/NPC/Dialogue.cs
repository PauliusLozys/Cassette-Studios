using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Transform dialogue;
    private Collider2D collider2D;
    private Transform dialogueContainer;
    private Transform dialogueTemplate;
    private bool isDialogueOpen = false;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        dialogueContainer = GameObject.Find("dialogueContainer").GetComponent<Transform>();
        dialogueTemplate = dialogueContainer.Find("dialogueTemplate");
        dialogueTemplate.gameObject.SetActive(isDialogueOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (collider2D != null && Input.GetKeyDown(KeyCode.C))
        {
            dialogueTemplate.Find("dialogueText").GetComponent<TextMeshProUGUI>().SetText(text);
            isDialogueOpen = true;
            Debug.Log("Dialogue opened");
            dialogueTemplate.gameObject.SetActive(isDialogueOpen);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isDialogueOpen)
        {
            isDialogueOpen = false;
            Debug.Log("Dialogue closed");
            dialogueTemplate.gameObject.SetActive(isDialogueOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collider2D = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDialogueOpen = false;
            collider2D = null;
            Debug.Log("Player ran away");
            dialogueTemplate.gameObject.SetActive(isDialogueOpen);
        }
    }
}
