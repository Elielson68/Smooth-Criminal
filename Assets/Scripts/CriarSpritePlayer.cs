using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody))]
public class CriarSpritePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Rigidbody rgdb;
    public Rigidbody Rgdb { get => rgdb; set => rgdb = value; }
    public Player _photonPlayer;
    private int _id;
    public Animator animator;
    [PunRPC]
    public void Inicializa(Player player)
    {
        _photonPlayer = player;
        _id = player.ActorNumber;
        GameManager.Instance.Jogadores.Add(this);
        
        if (player.IsMasterClient)
        {
            SpriteRenderer Host = GetComponent<SpriteRenderer>();
            Texture2D Personagem = Resources.Load<Texture2D>("Personagens/personagem 1/idle/1");
            Sprite PersonagemSprite = Sprite.Create(Personagem, new Rect(0.0f, 0.0f, Personagem.width, Personagem.height), new Vector2(0.0f, 0.0f), 100.0f);
            Host.sprite = PersonagemSprite;
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Personagens/personagem 1/parado/HostAnim");
        }
        else
        {
            SpriteRenderer Client = GetComponent<SpriteRenderer>();
            Texture2D Personagem = Resources.Load<Texture2D>("Personagens/personagem 2/idle/1");
            Sprite PersonagemSprite = Sprite.Create(Personagem, new Rect(0.0f, 0.0f, Personagem.width, Personagem.height), new Vector2(0.0f, 0.0f), 100.0f);
            Client.sprite = PersonagemSprite;
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Personagens/personagem 2/andando/ClientAnim");
        }
        if (!photonView.IsMine)
        {
            Rgdb.isKinematic = true;
        }
    }
}
