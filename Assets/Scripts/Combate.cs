using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Lutando();
        }
        else
        {
            anim.SetBool("socando", false);
        }
    }

    void Lutando()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Socando();
        }
        else
        {
            anim.SetBool("socando", false);
        }
        
    }

    void Socando()
    {
        anim.SetBool("socando", true);
    }
}
