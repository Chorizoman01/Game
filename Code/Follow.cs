using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    private Transform player;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //this will gaurantee it happens after player position updated
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = player.position.x + 5;
        transform.position = cameraPosition;


    }

}
