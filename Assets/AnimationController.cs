using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class AnimationController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject Punho;
    public void Bater(){
        photonView.RPC("RPCBater", RpcTarget.All);
    }
    [PunRPC]
    public void RPCBater(){
        Punho.SetActive(true);
    }
    [PunRPC]
    public void AnimationLevandoDano(){
        GetComponent<Animator>().SetBool("levandoDano", true);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
        { 
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.collider);
        }
    }
}
