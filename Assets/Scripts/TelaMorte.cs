using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaMorte : MonoBehaviour
{
    public void VoltarTelaInicial(){
        GestorDeRede.Instance.SairDaSala();
        GameManager.Instance.VoltarTelaInicial();
    }
}
