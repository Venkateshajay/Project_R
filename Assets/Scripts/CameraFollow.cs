using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Transform Camera;
    [SerializeField] Vector3 Offset;
    PlayerMovement PlayerMovementScript;
    bool changeCameraOffset;

    void Start()
    {
        Offset.x = 4.71f;
        Offset.y = 3f;
        Offset.z = -15;
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();
        Camera = GetComponent<Transform>();
    }
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + Offset.x, Camera.position.y , player.position.z + Offset.z);
        MoveCamera();
        CameraMovement();
        
    }

    void MoveCamera()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Offset.x < 4.71)
        {
            changeCameraOffset = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Offset.x > -4.71)
        {
            changeCameraOffset = true;
        }
    }

    void CameraMovement()
    {
        if (!changeCameraOffset && Offset.x <= 4.71)
        {
            Offset.x += .007f;
        }
        if(changeCameraOffset && Offset.x > -4.71f)
        {
            Offset.x -= .007f;
        }

    }
}
