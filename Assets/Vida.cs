using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Vida : MonoBehaviourPunCallbacks
{
    private ProgressBar barraVida;
    public ProgressBar Pb { get { return barraVida; } set { barraVida = value; } }
    public GameObject posVida;
    private GameObject prefabCriado;
    //public Camera cam;
    public float vidaAtual = 100;
    public Color corVida;
    public Color corVidaCritica;
    
    private void Start() {
        CreateLifeBar(); 
    }

    void CreateLifeBar(){
        GameObject prefab = Resources.Load<GameObject>("ProgressBar/UI ProgressBar");
        prefabCriado = Canvas.Instantiate<GameObject>(prefab, posVida.transform.position, posVida.transform.rotation, Canvas.FindObjectOfType<Canvas>().transform);
        /* prefabCriado.GetComponent<ProgressBar>().BarColor = corVida;
        prefabCriado.GetComponent<ProgressBar>().BarAlertColor = corVidaCritica;
        prefabCriado.GetComponent<ProgressBar>().BarValue = 100; */
        prefabCriado.transform.position = new Vector3(-260, -67, 0);
        Pb = prefabCriado.GetComponent<ProgressBar>();
        Pb.BarColor = corVida;
        Pb.BarAlertColor = corVidaCritica;
        Pb.BarValue = vidaAtual;
        Pb.Title = tag == "Player" ? photonView.Owner.NickName:"Fordo";

    }
    
    void FixedUpdate() {
        if(Pb.BarValue <= 0){
            photonView.RPC("VerificarMorte", RpcTarget.All);
        }
        Vector3 Posicao = Camera.FindObjectOfType<Camera>().WorldToScreenPoint(posVida.transform.position);
        prefabCriado.transform.position = Posicao;
    }
    [PunRPC]
    void VerificarMorte(){
        gameObject.GetComponent<Animator>().SetBool("morrendo", true);
        prefabCriado.SetActive(false);
    }
    
    [PunRPC]
    public void vidaPlayer(int value){
        Pb.BarValue -= value;
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
        }
        RPCDanoLevado();
    }
    void RPCDanoLevado(){
        Debug.LogError($"Quantidade de inimigos: {GameManager.Instance.QuantidadeDeInimigos()}");
        if(tag == "Inimigo"){
            GameManager.Instance.photonView.RPC("RetirarInimigo", RpcTarget.All);
            Debug.LogError($"Quantidade de inimigos após retirar 1 inimigo: {GameManager.Instance.QuantidadeDeInimigos()}");
        }
        else if(tag == "Player"){
             GameManager.Instance.RetirarJogadorDeLista(gameObject);
        }
        if(GameManager.Instance.QuantidadeDeInimigos() == 0){
            Debug.LogError("Minha tag é: "+tag);
            Round.Instance.photonView.RPC("AtualizarRound", RpcTarget.All);
            int quantidadeInimigosParaSpawnar = (int) Mathf.Pow(2, Round.Instance.round);
            for(int x=0;x<quantidadeInimigosParaSpawnar;x++){
                HordarController.Instance.CriarInimigo();
                GameManager.Instance.photonView.RPC("AdicionarInimigo", RpcTarget.All);
            }
            Debug.LogError($"Quantidade de inimigos após rodar o loop for: {GameManager.Instance.QuantidadeDeInimigos()}");   
        }
        else if(GameManager.Instance.QuantidadeDePlayersEmCena() == 0){
            Timer.Instance.photonView.RPC("PararTimer", RpcTarget.All);
        }
        if(tag == "Player"){
            PhotonNetwork.DestroyPlayerObjects(GetComponent<CriarSpritePlayer>()._id, false);
        }
        else{
            PhotonNetwork.Destroy(gameObject);
        }
        
        Destroy(prefabCriado);
    }

    public void PararDano(){
        photonView.RPC("RPCPararDano", RpcTarget.All);
    }
    [PunRPC]
    public void RPCPararDano(){
        gameObject.GetComponent<Animator>().SetBool("levandoDano", false);
    }

    
}
