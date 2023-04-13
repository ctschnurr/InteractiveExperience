using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement_2D : MonoBehaviour
{   
    [Header("Input settings:")]
    public float speedMultiplier = 5.0f;

    [Space]
    [Header("Character Stats:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    int redCoins = 0;
    TMP_Text redCoinText;

    int blueCoins = 0;
    TMP_Text blueCoinText;

    Image pizzaIcon;
    Color pizzaColor;
    bool hasPizza = false;

    Image gogglesIcon;
    Color gogglesColor;
    bool hasGoggles = false;

    Image keyIcon;
    Color keyColor;
    bool hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        redCoinText = GameObject.Find("RedCoinText").GetComponent<TMP_Text>();
        redCoinText.text = ("x " + redCoins);

        blueCoinText = GameObject.Find("BlueCoinText").GetComponent<TMP_Text>();
        blueCoinText.text = ("x " + blueCoins);

        pizzaIcon = GameObject.Find("PizzaIcon").GetComponent<Image>();
        pizzaColor = pizzaIcon.color;
        pizzaColor.a = 1f;

        gogglesIcon = GameObject.Find("GogglesIcon").GetComponent<Image>();
        gogglesColor = gogglesIcon.color;
        gogglesColor.a = 1f;

        keyIcon = GameObject.Find("KeyIcon").GetComponent<Image>();
        keyColor = keyIcon.color;
        keyColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
        Move();
        Animate();

        //Sets the idle to the last direction moved
        if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f || Input.GetAxis("Vertical") >= 0.1f || Input.GetAxis("Vertical") <= -0.1f)
        {
            animator.SetFloat("LastMoveX", Input.GetAxis("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxis("Vertical"));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * speedMultiplier;
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementSpeed);
    }

    public void AddItem(string name)
    {
        switch (name)
        {
            case "RedCoin":
                redCoins++;
                redCoinText.text = ("x " + redCoins);
                break;

            case "BlueCoin":
                blueCoins++;
                blueCoinText.text = ("x " + blueCoins);
                break;

            case "Pizza":
                pizzaIcon.color = pizzaColor;
                hasPizza = true;
                break;

            case "Goggles":
                gogglesIcon.color = gogglesColor;
                hasGoggles = true;
                break;

            case "Key":
                keyIcon.color = keyColor;
                hasKey = true;
                break;
        }
    }

    public void RemoveItem(string name)
    {
        switch (name)
        {
            case "RedCoin":
                redCoins = 0;
                redCoinText.text = ("x " + redCoins);
                break;

            case "BlueCoin":
                blueCoins = 0;
                blueCoinText.text = ("x " + blueCoins);
                break;

            case "Pizza":
                pizzaColor.a = 0f;
                pizzaIcon.color = pizzaColor;
                break;

            case "Goggles":
                gogglesColor.a = 0f;
                gogglesIcon.color = gogglesColor;
                break;

            case "Key":
                keyColor.a = 0f;
                keyIcon.color = keyColor;
                break;

        }
    }

    public bool ItemCheck(string item)
    {
        switch (item)
        {
            case "RedCoin":
                if (redCoins == 5) return true;
                else return false;

            case "BlueCoin":
                if (blueCoins == 5) return true;
                else return false;

            case "Pizza":
                if (hasPizza) return true;
                else return false;

            case "Goggles":
                if (hasGoggles) return true;
                else return false;

            case "Key":
                if (hasKey) return true;
                else return false;

            default:
                return false;
        }
    }
}
