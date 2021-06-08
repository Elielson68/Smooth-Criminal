using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Vida : MonoBehaviourPunCallbacks
{
    [SerializeField] private ProgressBar BarraVida;
    public ProgressBar Pb { get { return BarraVida; } set { BarraVida = value; } }
    public GameObject PrefabBar;
    public GameObject posVida;
    private GameObject prefabCriado;
    //public Camera cam;
    public float vidaAtual = 100;
    void Start() {
        
        prefabCriado = Canvas.Instantiate<GameObject>(PrefabBar, posVida.transform.position, posVida.transform.rotation, Canvas.FindObjectOfType<Canvas>().transform);
        prefabCriado.transform.position = new Vector3(-260, -67, 0);
        Pb = prefabCriado.GetComponent<ProgressBar>();
        Pb.BarValue = vidaAtual;
    }

    void FixedUpdate() {
        
         if (photonView.IsMine)
        {
            photonView.RPC("VerificarMorte", RpcTarget.All);
            photonView.RPC("MovimentarVida", RpcTarget.All);
            photonView.RPC("vidaPlayer", RpcTarget.All);
        }

    }
    [PunRPC]
    void VerificarMorte(){
        if(vidaAtual == 0){
            gameObject.GetComponent<Animator>().SetBool("morrendo", true);
            prefabCriado.SetActive(false);
        }
    }
    [PunRPC]
    void MovimentarVida(){
        
        Vector3 Posicao = Camera.FindObjectOfType<Camera>().WorldToScreenPoint(posVida.transform.position);
        prefabCriado.transform.position = Posicao;
    }
    [PunRPC]
    void vidaPlayer(){
        Pb.BarValue = vidaAtual;
    }
    
    void DanoLevado(){
        if (photonView.IsMine)
        {
            if(gameObject.tag == "Player"){
                Button Sair = Canvas.FindObjectOfType<Button>();
                Sair.GetComponent<Image>().enabled = true;
                Sair.GetComponent<Button>().enabled = true;
                Sair.GetComponentInChildren<Text>().text = "Sair";
            }
            photonView.RPC("RPCDanoLevado", RpcTarget.All);
        }
    }
    [PunRPC]
    void RPCDanoLevado(){
        
        Destroy(gameObject);
        Destroy(prefabCriado);
    }

}
