using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeRede : MonoBehaviourPunCallbacks
{
    public static GestorDeRede Instance { get; private set; }

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
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado!");
    }


    public void CriaSala(string nomeSala)
    {
        PhotonNetwork.CreateRoom(nomeSala);
    }

    public void EntraSala(string nomeSala)
    {
        PhotonNetwork.JoinRoom(nomeSala);
    }

    public void MudaNick(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }

    public string ObterListaDeJogadores()
    {
        string lista = "";
        foreach(var player in PhotonNetwork.PlayerList)
        {
            lista += $"{player.NickName}\n";
        }
        return lista;
    }

    public bool DonoDaSala()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public void SairDoLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void ComecaJogo(string nomeCena)
    {
        PhotonNetwork.LoadLevel(nomeCena);
    }
}
