using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntrada : MonoBehaviour
{
    [SerializeField] private Text _nomeDoJogador, _nomeDaSala, _errorEntrarSala;


    public void CriaSala()
    {
        GestorDeRede.Instance.MudaNick(_nomeDoJogador.text);
        GestorDeRede.Instance.CriaSala(_nomeDaSala.text);
    }
    public void EntraSala()
    {
        GestorDeRede.Instance.MudaNick(_nomeDoJogador.text);
        GestorDeRede.Instance.EntraSala(_nomeDaSala.text);
    }

    public void VerificarJogadores(){
        Debug.Log(GestorDeRede.Instance.ObterQuantidadeDeJogadores());
        if(GestorDeRede.Instance.ObterQuantidadeDeJogadores() > 2){
            _errorEntrarSala.text = "SALA CHEIA!";
            _nomeDoJogador.text = "";
            _nomeDaSala.text = "";
        }
    }
}
