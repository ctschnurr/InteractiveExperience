using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChatManager : MonoBehaviour
{

    public GameObject chatBox;
    public TMP_Text chatText;
    GameObject playerObject;
    Animator playerAnimator;
    PlayerMovement_2D player;

    Queue<string> chatScript;
    bool controlsPaused = false;

    string item = null;

    bool pizzaQuestComplete = false;
    bool guardQuestComplete = false;
    bool gogglesQuestComplete = false;
    bool keyQuestComplete = false;
    bool doorQuestComplete = false;

    GameObject gate;
    GameObject goggles;
    GameObject key;

    GameObject winMessage;

    // Start is called before the first frame update
    void Start()
    {
        chatScript = new Queue<string>();

        playerObject = GameObject.Find("Player");
        playerAnimator = GameObject.Find("Player/Sprites/Character").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement_2D>();
        gate = GameObject.Find("Gate");
        goggles = GameObject.Find("Goggles");
        key = GameObject.Find("Key");
        winMessage = GameObject.Find("WinMessage");
        winMessage.SetActive(false);
    }

    public void StartChat(string[] chatStrings, string name)
    {
        switch (name)
        {
            case "Pizza":
                item = "RedCoin";
                bool has5RedCoins = player.ItemCheck(item);
                Array.Resize(ref chatStrings, 2);
                if (!pizzaQuestComplete)
                {
                    
                    chatStrings[0] = "Pizza Slimes: \"You want some of our pizza?\"";
                    chatStrings[1] = "Pizza Slimes: \"You can have a slice for 5 red coins!\"";

                    if (has5RedCoins)
                    {
                        Array.Resize(ref chatStrings, 3);
                        chatStrings[2] = "Pizza Slimes: \"Oh, you have 5 red coins! I'll take those! Here's your pizza!\"";
                        player.RemoveItem(item);
                        string reward = "Pizza";
                        player.AddItem(reward);
                        pizzaQuestComplete = true;
                    }
                }
                else if (pizzaQuestComplete)
                {
                    Array.Resize(ref chatStrings, 1);
                    chatStrings[0] = "Pizza Slimes: \"Thanks for the coins! Enjoy your pizza!\"";
                }

                RunChat(chatStrings);
                break;

            case "BlueGuard":
                item = "Pizza";
                bool hasPizza = player.ItemCheck(item);
                Array.Resize(ref chatStrings, 2);
                if (!guardQuestComplete)
                {

                    chatStrings[0] = "Blue Guard: \"You want to pass? I'll open the gate for you if you find me something to eat!\"";
                    chatStrings[1] = "Blue Guard: \"A slice of pizza would really hit the spot!\"";

                    if (hasPizza)
                    {
                        Array.Resize(ref chatStrings, 3);
                        chatStrings[2] = "Blue Guard: \"You have pizza! For me? I'll open the gate!\"";
                        player.RemoveItem(item);
                        Destroy(gate);
                        guardQuestComplete = true;
                    }
                }
                else if (guardQuestComplete)
                {
                    Array.Resize(ref chatStrings, 1);
                    chatStrings[0] = "Blue Guard: \"Thank you for the pizza!\"";
                }

                RunChat(chatStrings);
                break;

            case "BlueSwimmer":
                item = "BlueCoin";
                bool has5BlueCoins = player.ItemCheck(item);
                Array.Resize(ref chatStrings, 2);
                if (!gogglesQuestComplete)
                {

                    chatStrings[0] = "Blue Swimmer: \"I found these goggles! Do you want them?\"";
                    chatStrings[1] = "Blue Swimmer: \"I'll sell them to you for 5 blue coins!\"";

                    if (has5BlueCoins)
                    {
                        Array.Resize(ref chatStrings, 3);
                        chatStrings[2] = "Blue Swimmer: \"5 blue coins? Nice! I'll take those! Here are your goggles!\"";
                        player.RemoveItem(item);
                        string reward = "Goggles";
                        player.AddItem(reward);
                        Destroy(goggles);
                        gogglesQuestComplete = true;
                    }
                }
                else if (gogglesQuestComplete)
                {
                    Array.Resize(ref chatStrings, 1);
                    chatStrings[0] = "Blue Swimmer: \"Thanks for the coins! Have fun with the goggles!\"";
                }

                RunChat(chatStrings);
                break;

            case "RedSwimmer":
                item = "Goggles";
                bool hasGoggles = player.ItemCheck(item);
                Array.Resize(ref chatStrings, 2);
                if (!keyQuestComplete)
                {

                    chatStrings[0] = "Red Swimmer: \"I lost my goggles.. will you find them for me?\"";
                    chatStrings[1] = "Red Swimmer: \"If you do, I'll give you this key I found!\"";

                    if (hasGoggles)
                    {
                        Array.Resize(ref chatStrings, 3);
                        chatStrings[2] = "Red Swimmer: \"My goggles! Thank you! Here is your key!\"";
                        player.RemoveItem(item);
                        string reward = "Key";
                        player.AddItem(reward);
                        Destroy(key);
                        keyQuestComplete = true;
                    }
                }
                else if (keyQuestComplete)
                {
                    Array.Resize(ref chatStrings, 1);
                    chatStrings[0] = "Red Swimmer: \"Thank you for finding my goggles!\"";
                }

                RunChat(chatStrings);
                break;

            case "Door":
                item = "Key";
                bool hasKey = player.ItemCheck(item);
                Array.Resize(ref chatStrings, 1);
                if (!doorQuestComplete)
                {
                    if (!hasKey) chatStrings[0] = "The door is locked.. you must find the key to escape the dungeon!";

                    if (hasKey) doorQuestComplete = true;
                }
                
                if (doorQuestComplete)
                {
                    winMessage.SetActive(true);
                }
                else if (!doorQuestComplete) RunChat(chatStrings);
                break;
            default:
                RunChat(chatStrings);
                break;
        }

    }

    public void RunChat(string[] goStrings)
    {
        chatScript.Clear();
        chatBox.SetActive(true);

        TogglePauseControls();

        foreach (string line in goStrings)
        {
            chatScript.Enqueue(line);
        }

        NextPage();
    }

    public void NextPage()
    {
        if(chatScript.Count == 0)
        {
            EndChat();
            return;
        }

        string line = chatScript.Dequeue();

        chatText.text = line;
    }

    void EndChat()
    {
        chatBox.SetActive(false);
        chatScript.Clear();

        TogglePauseControls();
    }

    void TogglePauseControls()
    {
        if (!controlsPaused)
        {
            playerObject.GetComponent<PlayerMovement_2D>().enabled = false;
            playerObject.GetComponent<PlayerInteraction>().enabled = false;

            controlsPaused = true;

            playerAnimator.SetFloat("Speed", 0);
            playerObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        else if (controlsPaused)
        {
            playerObject.GetComponent<PlayerMovement_2D>().enabled = true;
            playerObject.GetComponent<PlayerInteraction>().enabled = true;

            controlsPaused = false;
        }

    }
}
