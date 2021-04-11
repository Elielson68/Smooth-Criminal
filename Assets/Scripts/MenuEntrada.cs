using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntrada : MonoBehaviour
{
    [SerializeField] private Text _nomeDoJogador, _nomeDaSala;


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
}
