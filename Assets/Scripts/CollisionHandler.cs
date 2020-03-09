using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {

        HandleCollision(collision.gameObject.name);


    }

    private void HandleCollision(string collidedGameObjectName)
    {
        SendMessage("OnPlayerCollision");
        Debug.Log("Player collided with " + collidedGameObjectName);
    }
}
