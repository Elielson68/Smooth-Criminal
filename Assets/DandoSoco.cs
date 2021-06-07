using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandoSoco : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        other.GetComponent<Vida>().vidaAtual -= 50;
        other.GetComponent<Animator>().SetBool("levandoDano", true);
        gameObject.SetActive(false);
    }
    
}
