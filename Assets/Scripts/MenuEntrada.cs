using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
public class MenuEntrada : MonoBehaviour
{
    
    [SerializeField] private Text _nomeDoJogador, _nomeDaSala;

    public void CriaSala()
    {
        GestorDeRede.Instance.MudaNick(_nomeDoJogador.text);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        GestorDeRede.Instance.CriaSala(_nomeDaSala.text, roomOptions);

    }
    public void EntraSala()
    {
        GestorDeRede.Instance.MudaNick(_nomeDoJogador.text);
        GestorDeRede.Instance.EntraSala(_nomeDaSala.text);
    }

    public void LimparCampos(){
        _nomeDoJogador.text = "";
        _nomeDaSala.text = "";
    }
}
