using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private MenuEntrada _menuEntrada;
    [SerializeField] private MenuLobby _menuLobby;
    [SerializeField] private Text _errorMenuEntrada;
    private void Start()
    {
        _menuEntrada.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        _menuEntrada.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        MudaMenu(_menuLobby.gameObject);
        _menuLobby.photonView.RPC("AtualizaLista", RpcTarget.All);
        
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _errorMenuEntrada.text = "SALA CHEIA!";
        _menuEntrada.LimparCampos();
        
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorMenuEntrada.text = "NOME DA SALA JÁ EXISTE!";
        _menuEntrada.LimparCampos();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _menuLobby.AtualizaLista();
    }
    public void MudaMenu(GameObject menu)
    {
        _menuEntrada.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void SairDoLobby()
    {
        GestorDeRede.Instance.SairDaSala();
        MudaMenu(_menuEntrada.gameObject);
    }

    public void ComecaJogo(string nomeCena)
    {
        GestorDeRede.Instance.photonView.RPC("ComecaJogo", RpcTarget.All, nomeCena);
    }
}
