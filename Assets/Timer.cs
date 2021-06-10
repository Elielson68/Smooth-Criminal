using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Timer : MonoBehaviourPunCallbacks
{
    float segundos = 0;
    float minutos = 0;
    float hour = 0;
    bool timeUp = true;
    public static Timer Instance { get; private set; }
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
    private void FixedUpdate() {
        segundos =  timeUp ? segundos+Time.fixedDeltaTime : segundos;
        if(segundos > 59){
            segundos = 0;
            minutos++;
        } 
        if(minutos > 59){
            minutos = 0;
            hour++;
        }
        if(hour != 0){
            GetComponent<Text>().text = $"Tempo: {hour:00}:{minutos:00}:{segundos:00}";
        }
        else if(minutos != 0 ){
            GetComponent<Text>().text = $"Tempo: {minutos:00}:{segundos:00}";
        }
        else{
            GetComponent<Text>().text = $"Tempo: {segundos:00}";
        }
    }

    [PunRPC]
    public void PararTimer(){
        timeUp = false;
    }
}
