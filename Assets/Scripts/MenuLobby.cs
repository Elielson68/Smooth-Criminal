using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLobby : MonoBehaviour
{
    [SerializeField] private Text _listaDeJogadores;
    [SerializeField] private Button _comecaJogo;

    public void AtualizaLista()
    {

        _listaDeJogadores.text = GestorDeRede.Instance.ObterListaDeJogadores();
        _comecaJogo.interactable = GestorDeRede.Instance.DonoDaSala();
    }
}
