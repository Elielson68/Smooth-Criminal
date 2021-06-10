using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InimigoIA : MonoBehaviourPunCallbacks
{
    private Vector3 smooth = Vector3.zero;
    [SerializeField] private GameObject Punho;
    [SerializeField] private float timerFollowSleep = 1.8f;
    [SerializeField] private Animator animacao;
    [SerializeField] private Transform Inimigo;
    void Start()
    {
        Punho.SetActive(false);
    }

    [PunRPC]
    void IACombate(){
        RaycastHit hit;
        bool colidiu = Physics.SphereCast(gameObject.transform.position, 3f, Vector3.forward*3, out hit, 3f, 6);
        Debug.Log(colidiu);
        if (colidiu){
            Debug.Log(hit.distance);
            if(hit.collider.tag == "Player"){
                Vector3 moveAt = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, moveAt, ref smooth, 1f);
            }
            
            /* if(hit.transform.position.x < transform.position.x){
                transform.localEulerAngles= new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
            }
            else{
                transform.localEulerAngles= new Vector3(transform.localEulerAngles.x, -180, transform.localEulerAngles.z);
            } */
            if(hit.distance < 0.5){
                animacao.SetBool("atacando", true);
                timerFollowSleep = animacao.GetCurrentAnimatorStateInfo(0).length;
            }
            else{
                Punho.SetActive(false);
                animacao.SetBool("parado", false);
                animacao.SetBool("atacando", false);
                if(timerFollowSleep < 1.8f){
                    timerFollowSleep += Time.deltaTime;
                    
                }
                if (timerFollowSleep >= 1.8f){
                    
                    
                }
                
            }
        }
        else{
            animacao.SetBool("parado", true);
        }
    }

    [PunRPC]
    void AnimationAttack(){
        animacao.SetBool("atacando", true);
    }
    [PunRPC]
    void AnimationWalk(){
        Punho.SetActive(false);
        animacao.SetBool("parado", false);
        animacao.SetBool("atacando", false);
    }
    [PunRPC]
    void AnimationStop(){
        animacao.SetBool("parado", true);
        animacao.SetBool("atacando", false);
    }
    [PunRPC]
    private void MoveInimigo(float x, float y, float z){
        Vector3 player = new Vector3(x,y,z);
        
        
        float distance = Vector3.Distance(Inimigo.position, player);
        if(distance < 2){
            if(distance < 0.5){
                photonView.RPC("AnimationAttack", RpcTarget.All);
                timerFollowSleep = animacao.GetCurrentAnimatorStateInfo(0).length;
            }
            else{
                photonView.RPC("AnimationWalk", RpcTarget.All);
                if(timerFollowSleep < 1.8f){
                    timerFollowSleep += Time.deltaTime;
                }
                if (timerFollowSleep >= 1.8f){
                    Vector3 newPosition = Vector3.SmoothDamp(Inimigo.position, player, ref smooth, 1f); 
                    if(newPosition.x < Inimigo.position.x){
                        Inimigo.localEulerAngles= new Vector3(Inimigo.localEulerAngles.x, -180, Inimigo.localEulerAngles.z);
                    }
                    else{
                        Inimigo.localEulerAngles= new Vector3(Inimigo.localEulerAngles.x, 0, Inimigo.localEulerAngles.z);
                    } 
                    Inimigo.position = newPosition;
                        
                }
            
            }
        }
        else{
            photonView.RPC("AnimationStop", RpcTarget.All);
        }
        
    }
    
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player"){
            if(Inimigo.position.z < other.transform.position.z){
                Inimigo.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
            else{
                Inimigo.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            photonView.RPC("MoveInimigo", RpcTarget.All, other.transform.position.x, other.transform.position.y, other.transform.position.z);
        }
    }
    private void OntriggerExit(Collider other){
        photonView.RPC("AnimationStop", RpcTarget.All);
    }
}
