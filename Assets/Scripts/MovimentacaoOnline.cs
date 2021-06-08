using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentacaoOnline : MonoBehaviourPunCallbacks
{
    public Animator animator;
    [SerializeField] private Rigidbody rgdb;
    public Rigidbody Rgdb { get => rgdb; set => rgdb = value; }
    public GameObject Punho;
    public float velEixoZ;
    public float velEixoY;
    [SerializeField] private int Valor = 100;
    [SerializeField] public Transform _pontoReferenciaInferior;
    [SerializeField] public Transform _pontoReferenciaSuperior;
    void Start()
    {
        animator = GetComponent<Animator>();
        Rgdb = GetComponent<Rigidbody>();
        velEixoZ = 4f;
        velEixoY = 2f;
    }

    

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("CongelarMovimento", RpcTarget.All);
            if (Input.GetKey(KeyCode.F))
            {
                photonView.RPC("Socando", RpcTarget.All);
            }
            else if (!animator.GetBool("socando"))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    photonView.RPC("MovendoDireita", RpcTarget.All);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    photonView.RPC("MovendoEsquerda", RpcTarget.All);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    photonView.RPC("MovendoBaixo", RpcTarget.All);
                }
                if (Input.GetKey(KeyCode.W))
                {
                    photonView.RPC("MovendoCima", RpcTarget.All);
                }
            }
            if(!Input.anyKey)
            {
                photonView.RPC("CancelarAnimacoes", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void MovendoEsquerda()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        animator.SetBool("parado", false);
        Rgdb.velocity = new Vector2(-2f, 0f);
    }
    [PunRPC]
    void CongelarMovimento(){
        if(animator.GetBool("socando")){
            Rgdb.velocity = new Vector2(0f, 0f);
        }
    }
    [PunRPC]
    void MovendoDireita()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        Rgdb.velocity = new Vector2(2f, 0f);
    }
    [PunRPC]
    void MovendoCima()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        Rgdb.velocity = Vector3.forward * 2f;
    }
    [PunRPC]
    void MovendoBaixo()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        Rgdb.velocity = Vector3.forward * -2f;
    }
    [PunRPC]
    void Socando()
    {
        animator.SetBool("socando", true);
    }
    
    void SocandoBoxCollider(){
        if (photonView.IsMine)
        {
            photonView.RPC("RPCSocandoBoxCollider", RpcTarget.All);
        }
    }
    [PunRPC]
    void RPCSocandoBoxCollider(){
        Punho.SetActive(true);
    }
    
    void CancelandoBoxCollider(){
        if (photonView.IsMine)
        {
            photonView.RPC("RPCCancelandoBoxCollider", RpcTarget.All);
        }
    }
    [PunRPC]
    void RPCCancelandoBoxCollider(){
        Punho.SetActive(false);
    }
    [PunRPC]
    void CancelarAnimacoes()
    {
        animator.SetBool("parado", true);
        animator.SetBool("socando", false);
        Rgdb.velocity = new Vector2(0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Player")
      {
          Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.collider);
      }
    }


}
