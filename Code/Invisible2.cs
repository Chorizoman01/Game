using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible2 : MonoBehaviour
{

    public Color newColor;

    private SpriteRenderer rend;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            rend = GetComponent<SpriteRenderer>();
            rend.color = new Color(1, 1, 1, 1);

            NewJumpy jumpyMovement = collision.gameObject.GetComponent<NewJumpy>();


            if (GameManager.Instance != null)
            {
                jumpyMovement.animator.SetBool("Death", true);


                GameManager.Instance.Resetlvl(0.3f);
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }
        }
    }
}
