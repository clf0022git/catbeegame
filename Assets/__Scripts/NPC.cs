using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public GameObject textBox;
    public GameObject textBubble;
    private GameObject firstWing, secondWing;
    private GameObject canvas;

    private Text currentQuestText;
    Speech newSpeech = new Speech();
    public bool checkQuest = false;
    GameObject player, grabCollider;
    TextAsset theSpeech;
    IEnumerator talk;
    AudioSource audioSource;

    bool keyPressed = false;
    bool talking = false;
    public bool inRange = false;
    int speechNum = 1;
    int speechIndex = 0;
    bool finalTalk = false;

    void Start()
    {
        player = GameObject.Find("Player");
        grabCollider = GameObject.Find("GrabCollider"); 
        currentQuestText = GameObject.Find("ActualQuestText").GetComponent<Text>();
        audioSource = this.GetComponent<AudioSource>();
        firstWing = GameObject.Find("firstWing");
        secondWing = GameObject.Find("secondWing");
        canvas = GameObject.Find("EndGame");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            keyPressed = true;
        } 
        else if (keyPressed == true)
        {
            // Checks for the key quest
            if (this.name == "CatLadyBug2(Clone)" && inRange && grabCollider.GetComponent<PlayerGrabbing>().HoldingObject) 
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
            keyPressed = false;
        } 
        else if (Input.GetKeyDown(KeyCode.Q))
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


    IEnumerator Talk() // Coroutine goes through JSON string array until the last index then returns control to the player
    {
        theSpeech = Resources.Load<TextAsset>(speechPath + this.name + speechNum.ToString());
        newSpeech = JsonUtility.FromJson<Speech>(theSpeech.text);
        int speechLength = newSpeech.dialogue.Length;

        while (speechIndex < speechLength && talking &&!checkQuest) // All dialogue happens here
        {
            if (!audioSource.isPlaying && this.name != "Sign")
            {
                audioSource.Play();
            }
            textBox.GetComponent<TMP_Text>().text = newSpeech.dialogue[speechIndex];
            if (newSpeech.dialogue[speechIndex] == "It's my secret flying technique!")
            {
                firstWing.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 255);
                player.GetComponent<PlayerMovement>().firstWing = true;
                GameObject.Find("WingGetParticles").GetComponent<ParticleSystem>().Play();
            }
            else if (newSpeech.dialogue[speechIndex] == "Here take these wings so you can fly higher too!")
            {
                secondWing.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 255);
                player.GetComponent<PlayerMovement>().secondWing = true;
                GameObject.Find("WingGetParticles").GetComponent<ParticleSystem>().Play();
            }
            else if (newSpeech.dialogue[speechIndex] == "Let's go check it out!")
            {
                canvas.GetComponent<endGameScript>().unHide();
            } 
            else if (newSpeech.dialogue[speechIndex] == "Would you help me deliver to all of the villagers?")
            {
                canvas.GetComponent<endGameScript>().unHideDelivered();
            }
            else if (newSpeech.dialogue[speechIndex] == "I'm sure he'd appreciate some company!")
            {
                newSpeech.currentQuest = "Cross the rocky river!";
            }
            else if (newSpeech.dialogue[speechIndex] == "I'd say you should check that out, my dear fellow.")
            {
                newSpeech.currentQuest = "Climb the tree!";
            }
                speechIndex++;
            yield return WaitForKeyPress(KeyCode.E);
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

}