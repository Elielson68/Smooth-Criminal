using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Photon.Pun;
public class DetectSoco : MonoBehaviourPunCallbacks
{
    Vida aux_vida;
    void OnTriggerEnter(Collider other) {
        aux_vida=other.GetComponent<Vida>();
        if (photonView.IsMine){
            photonView.RPC("RetirarVidaPlayer", RpcTarget.All);
        }
    }
    [PunRPC]
    void RetirarVidaPlayer(){
        aux_vida.vidaAtual -= 10;
        gameObject.SetActive(false);
    }


}
