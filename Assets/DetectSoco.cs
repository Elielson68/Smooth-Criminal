using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSoco : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        other.GetComponent<Vida>().vidaAtual -= 10;
        gameObject.SetActive(false);
    }
}
