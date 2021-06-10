using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Round : MonoBehaviour
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

    public void AtualizarRound(){
        round++;
    }
}
