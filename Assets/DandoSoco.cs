using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class DandoSoco : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    float vidaAtual = 0;
    Animator aux_anim;
    Vida aux_vida;
    void OnTriggerEnter(Collider other) {
        aux_vida = other.GetComponent<Vida>();
        aux_anim = other.GetComponent<Animator>();
        float dist = Mathf.Abs(transform.position.z - other.transform.position.z); 
        if(dist < 0.5f){
            photonView.RPC("DarSoco", RpcTarget.All, 2);
        }
        
        
    }
    [PunRPC]
    void DarSoco(int vidaretirada){
        aux_anim.SetBool("levandoDano", true);
        aux_vida.vidaAtual -= vidaretirada;
        gameObject.SetActive(false);
    }
    
}
