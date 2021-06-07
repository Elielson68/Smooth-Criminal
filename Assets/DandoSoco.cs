using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class DandoSoco : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        photonView.RPC("DarSoco", RpcTarget.All, other);
    }
    [PunRPC]
    void DarSoco(Collider other){
        other.GetComponent<Vida>().vidaAtual -= 50;
        other.GetComponent<Animator>().SetBool("levandoDano", true);
        gameObject.SetActive(false);
    }
    
}
