using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    Vector3 smooth = Vector3.zero;
    private Animator animacao;
    public float timerFollowSleep = 1.8f;
    void Start()
    {
        animacao = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.right*3, Color.green);
        Debug.DrawRay(gameObject.transform.position, Vector3.right/1.9f, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector3.left*3, Color.green);
        Debug.DrawRay(gameObject.transform.position, Vector3.left, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.right*3,out hit, 3f)){
            if(hit.distance < 0.5){
                animacao.SetBool("atacando", true);
                timerFollowSleep = animacao.GetCurrentAnimatorStateInfo(0).length;
            }
            else{
                Debug.Log(animacao.IsInTransition(0));
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
}
