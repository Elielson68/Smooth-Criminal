using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _listaDeJogadores;
    [SerializeField] private Button _comecaJogo;

    [PunRPC]
    public void AtualizaLista()
    {

        _listaDeJogadores.text = GestorDeRede.Instance.ObterListaDeJogadores();
        _comecaJogo.interactable = GestorDeRede.Instance.DonoDaSala();
    }
}
