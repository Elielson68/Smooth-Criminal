using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentacao : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rgdb;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rgdb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Movendo();
        }
        else
        {
            animator.SetBool("parado", true);
            rgdb.velocity = new Vector2(0, 0);
        }


    }
    void Movendo()
    {
        if (Input.GetKey(KeyCode.D))
        {
            MovendoDireita();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovendoEsquerda();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovendoBaixo();
        }
        if (Input.GetKey(KeyCode.W))
        {
            MovendoCima();
        }
    }

    void MovendoEsquerda()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        animator.SetBool("parado", false);
        rgdb.velocity = new Vector2(-2f, 0f);
    }

    void MovendoDireita()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        rgdb.velocity = new Vector2(2f, 0f);
    }

    void MovendoCima()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        rgdb.velocity = new Vector2(0f, 2f);
    }

    void MovendoBaixo()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        rgdb.velocity = new Vector2(0f, -2f);
    }

}
