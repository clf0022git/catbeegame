using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [System.Serializable]
    public class Speech
    {
        public string[] dialogue;
        public bool questGiven = false;
        public string currentQuest = "";
    }

    public int amountOfSpeeches = 1;
    public bool cycleSpeeches = true;
    public string speechPath;   // The rest of these need to be connected in the editor
    public GameObject buttonPrompt; 
    public GameObject textBox;
    public GameObject textBubble;

    private Text currentQuestText;
    Speech newSpeech = new Speech();
    public bool checkQuest = false;
    GameObject player, grabCollider;
    TextAsset theSpeech;
    IEnumerator talk;

    bool talking = false;
    bool inRange = false;
    int speechNum = 1;
    int speechIndex = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        grabCollider = GameObject.Find("GrabCollider"); 
        currentQuestText = GameObject.Find("ActualQuestText").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Checks for the key quest
            if (inRange && grabCollider.GetComponent<PlayerGrabbing>().HoldingObject) 
            {
                Debug.Log("Quest Checked.");
                this.transform.GetComponent<NPCQuest>().CheckQuest(this.name + "Item");
            }

            if (player.GetComponent<PlayerMovement>().canMove && !talking && inRange) // Start dialogue
            {
                if (player.transform.position.x > this.transform.position.x) // Check position of player to face the correct direction
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;
                    textBubble.transform.localPosition = new Vector3(1, 0.5f, 0);
                    textBubble.transform.localScale = new Vector3(-1, 1, 1);
                    textBox.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                    textBubble.transform.localPosition = new Vector3(-1, 0.5f, 0);
                    textBubble.transform.localScale = new Vector3(1, 1, 1);
                    textBox.transform.localScale = new Vector3(1, 1, 1);
                }

                player.GetComponent<PlayerMovement>().canMove = false;
                talking = true;
                textBubble.GetComponent<SpriteRenderer>().enabled = true;
                talk = Talk();
                StartCoroutine(talk);
            }
            else // End dialogue early
            {
                if (talk != null)
                {
                    StopCoroutine(talk);
                    player.GetComponent<PlayerMovement>().canMove = true;
                    talking = false;
                    speechIndex = 0;
                    textBubble.GetComponent<SpriteRenderer>().enabled = false;
                    textBox.GetComponent<TMP_Text>().text = "";
                }
            }
        }
    }

    IEnumerator Talk() // Coroutine goes through JSON string array until the last index then returns control to the player
    {
        theSpeech = Resources.Load<TextAsset>(speechPath + this.name + speechNum.ToString());
        newSpeech = JsonUtility.FromJson<Speech>(theSpeech.text);
        int speechLength = newSpeech.dialogue.Length;

        while (speechIndex < speechLength && talking &&!checkQuest) // All dialogue happens here
        {
            textBox.GetComponent<TMP_Text>().text = newSpeech.dialogue[speechIndex];
            speechIndex++;
            yield return WaitForKeyPress(KeyCode.Mouse0);
        }

        // End of dialogue checklist
        textBox.GetComponent<TMP_Text>().text = "";
        textBubble.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerMovement>().canMove = true;
        speechIndex = 0;
        talking = false;
        if (speechNum < amountOfSpeeches) 
        {
            speechNum++;
        }
        else if (cycleSpeeches == true)
        {
            speechNum = 1;
        }

        if (newSpeech != null && newSpeech.currentQuest != "")
        {
            currentQuestText.text = newSpeech.currentQuest;
        }

        if (newSpeech != null && (newSpeech.questGiven == true || checkQuest == true))
        {
            Debug.Log("Change Cat");
            this.transform.GetComponent<NPCQuest>().CatQuest(this.name + "Start");
            newSpeech.questGiven = false;
        }

    }

    IEnumerator WaitForKeyPress(KeyCode key) // Wait for mouse click to continue dialogue
    {
        bool done = false;

        while (!done)
        {
            if (Input.GetKeyDown(key))
            {
                done = true;
            }
            yield return null;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("NPC Interaction enabled");
            inRange = true;
            buttonPrompt.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("NPC Interaction disabled");
            inRange = false;
            buttonPrompt.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}