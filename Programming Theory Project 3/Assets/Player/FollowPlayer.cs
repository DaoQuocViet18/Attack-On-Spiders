using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player1;

    private Vector3 offset = new Vector3(0, 0.8f, 0.3f);            // tr? ?i chi?u cao - chi?u dài c?a v?t th? --> vì trí camera mong mu?n

    void Start()
    {

    }

    void LateUpdate()
    {
        // offset the camera behind the player by adding to the player's position
        transform.position = player1.transform.position + offset;
    }
}
