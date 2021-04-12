using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentacaoOffline : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Rigidbody2D rgdb;
    public Rigidbody2D Rgdb { get => rgdb; set => rgdb = value; }

    [SerializeField] private ProgressBar pb;
    public ProgressBar Pb { get { return pb; } set { pb = value; } }
    [SerializeField] private int Valor = 100;
    [SerializeField] private Transform _pontoReferenciaInferior;
    [SerializeField] private Transform _pontoReferenciaSuperior;
    void Start()
    {
        animator = GetComponent<Animator>();
        Rgdb = GetComponent<Rigidbody2D>();
        Pb = Canvas.FindObjectOfType<ProgressBar>();

    }

    // Update is called once per frame
    void Update() 
    { 
        Pb.BarValue = Valor;
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
        Rgdb.velocity = new Vector2(0f, 2f);
        if(gameObject.transform.position.y >= _pontoReferenciaInferior.position.y && gameObject.transform.position.z <= _pontoReferenciaSuperior.position.z)
        {
            gameObject.transform.Translate(0, 0, 0.1f);
        }
        
        
    }
    void MovendoBaixo()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("parado", false);
        Rgdb.velocity = new Vector2(0f, -2f);
        if (gameObject.transform.position.y <= _pontoReferenciaSuperior.position.y && gameObject.transform.position.z <= _pontoReferenciaInferior.position.z)
        {

        }
    }
    void Socando()
    {
        animator.SetBool("socando", true);
    }

    void CancelarAnimacoes()
    {
        animator.SetBool("parado", true);
        animator.SetBool("socando", false);
        Rgdb.velocity = new Vector2(0, 0);
    }

}
