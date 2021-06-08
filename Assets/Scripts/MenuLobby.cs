using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _listaDeJogadores;
    [SerializeField] private Text _nomeSala;
    [SerializeField] private Text _quantidadeMaxJogadores;
    [SerializeField] private Button _comecaJogo;

    [PunRPC]
    public void AtualizaLista()
    {
        _listaDeJogadores.text = GestorDeRede.Instance.ObterListaDeJogadores();
        _comecaJogo.interactable = GestorDeRede.Instance.DonoDaSala();
        _nomeSala.text = GestorDeRede.Instance.ObterNomeDaSala();
        _quantidadeMaxJogadores.text = $"Quantidade de jogadores\n{GestorDeRede.Instance.ObterQuantidadeDeJogadores()}/2";
    }
}
