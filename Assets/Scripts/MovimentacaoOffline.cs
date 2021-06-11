    using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentacaoOffline : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Rigidbody rgdb;
    public Rigidbody Rgdb { get => rgdb; set => rgdb = value; }
    public GameObject Punho;
    [SerializeField] private Transform _pontoReferenciaInferior;
    [SerializeField] private Transform _pontoReferenciaSuperior;
    void Start()
    {
        animator = GetComponent<Animator>();
        Rgdb = GetComponent<Rigidbody>();
        

    }

    void FixedUpdate(){
        if (Input.anyKey)
        {

            Movendo();
        }
        else
        {
            CancelarAnimacoes();

        }
    }

    void Movendo()
    {
        if (Input.GetKey(KeyCode.D))
        {
            MovendoDireita();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovendoEsquerda();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovendoBaixo();
        }
        if (Input.GetKey(KeyCode.W))
        {
            MovendoCima();
        }
        if (Input.GetKey(KeyCode.F))
        {
            Socando();
        }
    }
    void MovendoEsquerda()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);

        animator.SetBool("parado", false);
        Rgdb.velocity = new Vector2(-2f, 0f);
    }
    void MovendoDireita()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        Rgdb.velocity = new Vector2(2f, 0f);
    }
    void MovendoCima()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        
        if(gameObject.transform.position.y >= _pontoReferenciaInferior.position.y && gameObject.transform.position.z <= _pontoReferenciaSuperior.position.z)
        {
            Rgdb.velocity = new Vector3(0f, 0, 2f);
            //gameObject.transform.Translate(0, 0, );
        }
        else
        {
            Rgdb.velocity = new Vector2(0f, 2f);
        }
        
        
    }
    void MovendoBaixo()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        
        if (gameObject.transform.position.y <= _pontoReferenciaSuperior.position.y && gameObject.transform.position.z >= _pontoReferenciaInferior.position.z)
        {
            Rgdb.velocity = new Vector3(0f, 0, -2f);
            //gameObject.transform.Translate(0, 0, -1f);
        }
        else
        {
            Rgdb.velocity = new Vector2(0f, -2f);
        }
    }
    void Socando()
    {
        animator.SetBool("socando", true);
    }
    void SocandoBoxCollider(){
        Punho.SetActive(true);
    }
    
    void CancelandoBoxCollider(){
        Punho.SetActive(false);
    }
    void CancelarAnimacoes()
    {
        Punho.SetActive(false);
        animator.SetBool("parado", true);
        animator.SetBool("socando", false);
        Rgdb.velocity = new Vector2(0, 0);
    }

}
