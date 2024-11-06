using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour
{
    public Color newColor;

    private SpriteRenderer rend;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            rend = GetComponent<SpriteRenderer>();
            rend.color = new Color(1, 1, 1, 1);

        }
    }
}
