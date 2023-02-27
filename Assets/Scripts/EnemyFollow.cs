using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Restart canvas is here

public class EnemyFollow : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float speed;
    [SerializeField] float idleSpeed;

    [Header("Position")]
    [SerializeField] float stopingDistance;
    [SerializeField] Transform EnemyPosition;
    [SerializeField] Transform PlayerPosition;
    [SerializeField] Transform Range;

    [Header("Collider")]
    [SerializeField] PolygonCollider2D left;
    [SerializeField] PolygonCollider2D right;

    [Header("Bool")]
    bool atRange=false;
    bool isEnemySeenThePlayer = false;
    public bool gameover = false;

    [Header("Script")]
    PlayerMovement playerMovementScript;

    [Header("Canvas")]
    [SerializeField] GameObject PlayUI;
    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();
    }
    void FixedUpdate()
    {
        EnemyAI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isEnemySeenThePlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isEnemySeenThePlayer = false;
        }
    }

    void EnemyAI()
    {
        if (Vector2.Distance(transform.position, PlayerPosition.position) < 10 && isEnemySeenThePlayer)
        {
            EnemyChase();
            if (Vector2.Distance(transform.position, PlayerPosition.position) < stopingDistance)
            {
                gameover = true;
                PlayUI.SetActive(true);
                playerMovementScript.FreezeMovement();
            }

        }
        else
        {
            EnemyMovement();
        }
    }

    void EnemyMovement()
    {
        if (!atRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, Range.position, idleSpeed * Time.deltaTime);
            isAtRange();
            left.enabled = true;
            right.enabled = false;
        }
        else
        {
            EnemyReturn();
            isAtRange();
            right.enabled = true;
            left.enabled = false;
        }
    }

    void EnemyChase()
    {
        if (left.enabled == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerPosition.position + new Vector3(1, 0, 0), speed * Time.deltaTime);
        }
        if (left.enabled == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerPosition.position - new Vector3(1, 0, 0), speed * Time.deltaTime);
        }
    }

    void EnemyReturn()
    {
        transform.position = Vector2.MoveTowards(transform.position, EnemyPosition.position, idleSpeed * Time.deltaTime);
    }

    void isAtRange()
    {
        if (transform.position == Range.position)
        {
            atRange = true;
        }
        if (transform.position == EnemyPosition.position)
        {
            atRange = false;
        }
    }
}
