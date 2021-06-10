using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class DandoSoco : MonoBehaviourPunCallbacks
{
    void OnTriggerEnter(Collider other) {
        if(photonView.IsMine){
            if(other.tag == "Inimigo"){
                other.GetComponent<Vida>().photonView.RPC("vidaPlayer", RpcTarget.All, 50);
                other.GetComponent<AnimationController>().photonView.RPC("AnimationLevandoDano", RpcTarget.All);
                photonView.RPC("DarSoco", RpcTarget.All);
            } 
        }
    }
    [PunRPC]
    void DarSoco(){
        gameObject.SetActive(false);
    }
}
