using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    public ProgressBar pb;
    public int Valor = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pb.BarValue = Valor;
    }
}
