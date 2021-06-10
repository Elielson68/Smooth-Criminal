using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DetectSoco : MonoBehaviourPunCallbacks
{
    void OnTriggerEnter(Collider other) {
        if(photonView.IsMine){
            other.GetComponent<Vida>().photonView.RPC("vidaPlayer", RpcTarget.All, 10);
            Debug.Log(other);
            photonView.RPC("DesativarBoxColliderPunho", RpcTarget.All);
        }
    }

    [PunRPC]
    public void DesativarBoxColliderPunho(){
        gameObject.SetActive(false);
    }


}
