using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class InimigoIA : MonoBehaviourPunCallbacks
{
    Vector3 smooth = Vector3.zero;
    private Animator animacao;
    public float timerFollowSleep = 1.8f;
    public GameObject Punho;
    void Start()
    {
        animacao = GetComponent<Animator>();
        Punho.SetActive(false);
    }

    void FixedUpdate()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.right*3, Color.green);
        Debug.DrawRay(gameObject.transform.position, Vector3.right/1.9f, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector3.left*3, Color.green);
        Debug.DrawRay(gameObject.transform.position, Vector3.left/1.9f, Color.red);
        photonView.RPC("IACombate", RpcTarget.All);
        
    }
    [PunRPC]
    void IACombate(){
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.right*3,out hit, 3f)){
            if(hit.distance < 0.5){
                animacao.SetBool("atacando", true);
                timerFollowSleep = animacao.GetCurrentAnimatorStateInfo(0).length;
            }
            else{
                Punho.SetActive(false);
                animacao.SetBool("parado", false);
                animacao.SetBool("atacando", false);
                transform.localEulerAngles= new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                if(timerFollowSleep < 1.8f){
                    timerFollowSleep += Time.deltaTime;
                    
                }
                if (timerFollowSleep >= 1.8f){
                    transform.position = Vector3.SmoothDamp(transform.position, hit.transform.position, ref smooth, 1f);
                }
                
            }
        }
        else if (Physics.Raycast(gameObject.transform.position, Vector3.left*3,out hit, 3f)){
            
            if(hit.distance < 0.5){
                animacao.SetBool("atacando", true);
                timerFollowSleep = animacao.GetCurrentAnimatorStateInfo(0).length;
                
            }
            else{
                Punho.SetActive(false);
                transform.localEulerAngles= new Vector3(transform.localEulerAngles.x, -180, transform.localEulerAngles.z);
                animacao.SetBool("parado", false);
                animacao.SetBool("atacando", false);
                if(timerFollowSleep <= 1.8f){
                    timerFollowSleep += Time.deltaTime;
                }

                if (timerFollowSleep >= 1.8f){
                    
                    transform.position = Vector3.SmoothDamp(transform.position, hit.transform.position, ref smooth, 1f);
                }
                
            }
        }
        else{
            animacao.SetBool("parado", true);
        }
    }
    public void Bater(){
        photonView.RPC("RPCBater", RpcTarget.All);
    }
    [PunRPC]
    public void RPCBater(){
        Punho.SetActive(true);
    }
    public void PararDano(){
        photonView.RPC("RPCPararDano", RpcTarget.All);
    }
    [PunRPC]
    public void RPCPararDano(){
        animacao.SetBool("levandoDano", false);
    }

    
}
