using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//victory canvas is here
public class PlayerMovement : MonoBehaviour
{
    [Header("Bool")]
    bool IsPlayerTouchingDoor = false;
    bool IsPlayerTouchingExit = false;
    bool canMove = true;

    [Header("Renders")]
    SpriteRenderer PlayerSpriteRenderer;
    private Rigidbody2D PlayerRigidbody;
    [SerializeField] float speed=.007f;
    private Vector2 MoveDirection;

    public int noOfCoins = 0;
    [SerializeField] GameObject Coin;
    [SerializeField] GameObject VictoryCanvas;
    void Start()
    {
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        MoveDirection = Vector2.zero;
        Movement();
        MakePlayerInvisible();
        MakePlayerVisible();
        ActivateVictoryCanvas();
    }

    void Movement()
    {
        if (canMove)
        {
            MoveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            MoveDirection *= speed;
            PlayerRigidbody.velocity = new Vector3(MoveDirection.x, MoveDirection.y, 0);
        }
        else
        {
            PlayerRigidbody.velocity = Vector2.zero;
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            IsPlayerTouchingDoor = true;
        }
        if (other.tag == "Coin")
        {
            noOfCoins++; 
            Destroy(Coin);
        }
        if (other.tag == "ExitDoor"  &&  noOfCoins>0)
        {
            IsPlayerTouchingExit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Door")
        {
            IsPlayerTouchingDoor = false;
        }
        if (other.tag == "ExitDoor")
        {
            IsPlayerTouchingExit = false;

        }
    }

    void MakePlayerInvisible()
    {
        if (Input.GetKey(KeyCode.Space) && IsPlayerTouchingDoor)
        {
            PlayerSpriteRenderer.enabled = false;
            tag = "InvisiblePlayer";
        }
        
    } 

    void MakePlayerVisible()
    {
        if (!IsPlayerTouchingDoor)
        {
            PlayerSpriteRenderer.enabled = true;
            tag = "Player";
        }
    }

    public void FreezeMovement()
    {
        canMove = false;
    }

    void ActivateVictoryCanvas()
    {
        if (IsPlayerTouchingExit &&  Input.GetKey(KeyCode.Backspace)) { VictoryCanvas.SetActive(true); }        
    }
}
