using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    ChatManager chatManager;

    public enum Behavior
    {
        blank,
        sign,
        pickUp,
        npc
    }

    public Behavior behavior;

    public string signContents;
    TMP_Text playerMessage;
    PlayerMovement_2D player;

    public string[] chatStrings;

    // Start is called before the first frame update
    void Start()
    {
        playerMessage = GameObject.Find("PlayerMessage").GetComponent<TMP_Text>();
        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement_2D>();
    }

    // Update is called once per frame
    public void Npc()
    {
        string name = this.gameObject.name;
        chatManager.StartChat(chatStrings, name);
    }

    public void Sign()
    {
        StartCoroutine(ShowSign(signContents, 2.5f));
    }

    IEnumerator ShowSign(string message, float delay)
    {
        playerMessage.text = message;
        yield return new WaitForSeconds(delay);
        playerMessage.text = null;
    }

    public void PickUp()
    {
        string name = this.gameObject.name;
        player.AddItem(name);
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);

    }

    public void Blank()
    {

    }


}
