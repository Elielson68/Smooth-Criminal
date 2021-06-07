using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }
    public List<movimentacao> Jogadores { get => _jogadores; private set => _jogadores = value; }

    [SerializeField] private string _localizacaoPrefab;
    [SerializeField] private Transform _spawnJogadores;
    [SerializeField] public GameObject _spawnInferior;
    [SerializeField] public GameObject _spawnSuperior;
    private int _jogadoresEmJogo;
    private List<movimentacao> _jogadores;

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
        _jogadores = new List<movimentacao>();
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
        _spawnJogadores.Translate(3,0,0);
        var inferior = Instantiate(_spawnInferior, _spawnInferior.transform.position, Quaternion.identity);
        var superior = Instantiate(_spawnSuperior, _spawnSuperior.transform.position, Quaternion.identity);
        var jogador = jogadorObj.GetComponent<movimentacao>();
        jogador._pontoReferenciaInferior = inferior.transform;
        jogador._pontoReferenciaSuperior = superior.transform;
        jogador.photonView.RPC("Inicializa", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
