using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }
    public List<CriarSpritePlayer> Jogadores { get => _jogadores; private set => _jogadores = value; }

    [SerializeField] private string _localizacaoPrefab;
    [SerializeField] private Transform _spawnJogadores;
    [SerializeField] public GameObject _spawnInferior;
    [SerializeField] public GameObject _spawnSuperior;
    [SerializeField] public Transform _spawnInimigos;
    private int _jogadoresEmJogo;
    private List<CriarSpritePlayer> _jogadores;
    private int quantidadeDeInimigosEmCena = 0;
    private List<GameObject> playersEmCena = new List<GameObject>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        if(PhotonNetwork.IsMasterClient){
            photonView.RPC("AdicionarInimigo", RpcTarget.AllBuffered);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        photonView.RPC("AdicionaJogador", RpcTarget.AllBuffered);
        _jogadores = new List<CriarSpritePlayer>();  
    }

    [PunRPC]
    private void AdicionaJogador()
    {
        _jogadoresEmJogo++;
        if(_jogadoresEmJogo == PhotonNetwork.PlayerList.Length)
        {
            CriaJogador();
        }
    }
    public void AdicionarJogadorEmLista(GameObject player){
        playersEmCena.Add(player);
    }
    public void RetirarJogadorDeLista(GameObject player){
        playersEmCena.Remove(playersEmCena.Find(x => x.Equals(player)));
    }
    public int QuantidadeDePlayersEmCena(){
        return playersEmCena.Count;
    }

    [PunRPC]
    public void AdicionarInimigo(){
        quantidadeDeInimigosEmCena++;
    }
    [PunRPC]
    public void RetirarInimigo(){
        quantidadeDeInimigosEmCena--;
    }
    public int QuantidadeDeInimigos(){
        return quantidadeDeInimigosEmCena;
    }
    [PunRPC]
    public void CriarInimigo(){
        float maxZ = 4.17f;
        float minZ = -1.09f;
        float minX = 5.34f;
        float maxX = 8.88f;
        float z = Random.Range(minZ, maxZ);
        float x = Random.Range(minX, maxX);
        _spawnInimigos.position = new Vector3(x, _spawnInimigos.position.y, z);
        PhotonNetwork.Instantiate("Inimigo2 Variant", _spawnInimigos.position, Quaternion.identity);
    }
    private void CriaJogador()
    {
        var jogadorObj = PhotonNetwork.Instantiate(_localizacaoPrefab, _spawnJogadores.position, Quaternion.identity);
        _spawnJogadores.Translate(5,0,0);
        var jogador = jogadorObj.GetComponent<CriarSpritePlayer>();
        jogadorObj.GetComponent<MovimentacaoOnline>()._pontoReferenciaInferior = _spawnInferior.transform;
        jogadorObj.GetComponent<MovimentacaoOnline>()._pontoReferenciaSuperior = _spawnSuperior.transform;
        jogador.photonView.RPC("Inicializa", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public void SairJogo(){
        GestorDeRede.Instance.ComecaJogo("Morte");
    }
    public void VoltarTelaInicial(){
        GestorDeRede.Instance.ComecaJogo("Menu");
    }
}
