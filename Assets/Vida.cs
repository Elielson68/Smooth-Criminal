using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] private ProgressBar BarraVida;
    public ProgressBar Pb { get { return BarraVida; } set { BarraVida = value; } }
    public GameObject PrefabBar;
    public Canvas canvinha;
    public GameObject posVida;
    private GameObject prefabCriado;
    public Camera cam;
    public float vidaAtual = 100;
    void Start() {
        prefabCriado = Canvas.Instantiate<GameObject>(PrefabBar, posVida.transform.position, posVida.transform.rotation, canvinha.transform);
        prefabCriado.transform.position = new Vector3(-260, -67, 0);
        Pb = prefabCriado.GetComponent<ProgressBar>();
        Pb.BarValue = vidaAtual;
    }

    void Update() {
        Pb.BarValue = vidaAtual;
        Vector3 smoth = Vector3.zero;
        Vector3 Posicao = cam.WorldToScreenPoint(posVida.transform.position);
        prefabCriado.transform.position = Posicao;
        if(vidaAtual == 0){
            gameObject.GetComponent<Animator>().SetBool("morrendo", true);
            prefabCriado.SetActive(false);
        }
    }
    void DanoLevado(){
        Destroy(gameObject);
        Destroy(prefabCriado);
    }

}
