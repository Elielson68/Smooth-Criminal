using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Round : MonoBehaviourPunCallbacks
{
    public int round = 1;
    public static Round Instance { get; private set; }
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
    private void Start() {
        round = 1;
    }
    private void FixedUpdate() {
        GetComponent<Text>().text = $"Round: {round}";    
    }
    [PunRPC]
    public void AtualizarRound(){
        round++;
    }
}
