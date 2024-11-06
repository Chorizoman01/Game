using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player_location;
    public float player_locationx;
    public float player_locationy;

    public void Update()
    {
        player_location = GameObject.Find("Jumpy");

        player_locationx = player_location.transform.position.x;

        player_locationy = player_location.transform.position.y;

        //Debug.Log("X= " + player_locationx + "Y= " + player_locationy);

    }



}
