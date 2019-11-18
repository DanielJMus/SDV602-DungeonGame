using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int ID;
    private Vector3 previousPosition;
    private Vector3 previousRotation;

    void Update () {
        if(moving) return;
        if(Input.GetKey(KeyCode.UpArrow)) {
            Move("forward");
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            Move("back");
        } else if(Input.GetKey(KeyCode.LeftArrow)) {
            Move("left");
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            Move("right");
        }

        GetDeviceTilt();
    }

    // Accelerometer input, user can tilt their phone to move around
    private float tiltThreshold = 0.25f;
    void GetDeviceTilt () {
        if(Accelerometer.Yaw > tiltThreshold) {
            Move("right");
        } else if(Accelerometer.Yaw < -tiltThreshold) {
            Move("left");
        } else if(Accelerometer.Pitch < -tiltThreshold * 3) {
            Move("forward");
        }
    }
    
    // Move the camera in the specified direction
    public void Move (string destination) {
        // OnItemTile = false;
        Vector3 newPosition = transform.position;
        Quaternion newRotation = transform.rotation;
        switch(destination)
        {
            case "forward":
                RaycastHit fwdHit;
                Physics.Raycast(transform.position, transform.forward, out fwdHit, 1f);
                if(fwdHit.collider == null || !fwdHit.collider.gameObject.CompareTag("Wall"))
                    newPosition = transform.position + transform.forward;
                break;
            case "back":
                RaycastHit bkdHit;
                Physics.Raycast(transform.position, -transform.forward, out bkdHit, 1f);
                if(bkdHit.collider == null || !bkdHit.collider.gameObject.CompareTag("Wall"))
                    newPosition = transform.position - transform.forward;
                break;
            case "left":
                newRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
                break;
            case "right":
                newRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
                break;
        }
        previousPosition = transform.position;
        previousRotation = transform.localEulerAngles;
        StartCoroutine(Animate(newPosition, newRotation));

    }

    // Smooth the movement between tiles
    bool moving = false;

    IEnumerator Animate (Vector3 _position, Quaternion _rotation)
    {
        moving = true;
        for(float i = 0.0f; i < 1.0f; i += Time.deltaTime * 2)
        {
            transform.position = Vector3.Lerp(transform.position, _position, i);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, i);
            yield return null;
        }
        transform.position = _position;
        transform.rotation = _rotation;
        moving = false;
    }

    // Item Tiles

    private bool OnItemTile;
    public bool IsOnItemTile {
        get { return OnItemTile; }
        set { OnItemTile = value; }
    }

    private ItemTile itemTile;
    public ItemTile GetItemTile {
        get { return itemTile; }
    }

    public void SetItemTile (ItemTile tile)
    {
        OnItemTile = true;
        itemTile = tile;
    }

    // Door Tiles

    private bool OnDoorTile;
    public bool IsOnDoorTile {
        get { return OnDoorTile; }
        set { OnDoorTile = value; }
    }

    private DoorTile doorTile;
    public DoorTile GetDoorTile {
        get { return doorTile; }
    }

    public void SetDoorTile (DoorTile tile)
    {
        OnDoorTile = true;
        doorTile = tile;
    }
}
