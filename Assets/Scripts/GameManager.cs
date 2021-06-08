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
    private int _jogadoresEmJogo;
    private List<CriarSpritePlayer> _jogadores;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
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
