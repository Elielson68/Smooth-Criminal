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
            if (Input.GetAxisRaw("Fire1")==1)
            {
                photonView.RPC("Socando", RpcTarget.All);
            }
            else if (!animator.GetBool("socando"))
            {
                photonView.RPC("Movendo", RpcTarget.All);
            }
            if(!Input.anyKey)
            {
                photonView.RPC("CancelarAnimacoes", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void CongelarMovimento(){
        if(animator.GetBool("socando")){
            Rgdb.velocity = new Vector2(0f, 0f);
        }
    }
    [PunRPC]
    void Movendo()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(horizontal!=0 || vertical!=0){
            animator.SetBool("parado", false);
        }
        Rgdb.velocity = new Vector3( horizontal* 2f, Rgdb.velocity.y, vertical * 2f);
        Vector3 rotacao = gameObject.transform.localEulerAngles;
        if(Input.GetKey(KeyCode.M)){
            
            if(horizontal==1){
                gameObject.transform.localEulerAngles = new Vector3(rotacao.x, -180, rotacao.z);
            }
            else if (horizontal==-1){
                gameObject.transform.localEulerAngles = new Vector3(rotacao.x, 0, rotacao.z);
            }
        }
        else{
            if(horizontal==1){
                gameObject.transform.localEulerAngles = new Vector3(rotacao.x, 0, rotacao.z);
            }
            else if (horizontal==-1){
                gameObject.transform.localEulerAngles = new Vector3(rotacao.x, -180, rotacao.z);
            }
        }
        
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
