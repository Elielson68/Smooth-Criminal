using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
public class GestorDeRede : MonoBehaviourPunCallbacks
{
    public static GestorDeRede Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(Instance==this){
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado!");
    }


    public bool CriaSala(string nomeSala, RoomOptions options)
    {
        return PhotonNetwork.CreateRoom(nomeSala, options);
    }

    public bool EntraSala(string nomeSala)
    {
        return PhotonNetwork.JoinRoom(nomeSala);
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
    public int ObterQuantidadeDeJogadores()
    {
        return PhotonNetwork.PlayerList.Length;
    }
    public string ObterNomeDaSala()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }
    public bool DonoDaSala()
    {
        
        return PhotonNetwork.IsMasterClient;
    }

    public void SairDaSala()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void ComecaJogo(string nomeCena)
    {
        PhotonNetwork.LoadLevel(nomeCena);
    }
}
