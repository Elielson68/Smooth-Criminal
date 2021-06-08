using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CreatorSingletonManagerRede : MonoBehaviourPunCallbacks
{
    private void Awake() {
        if(FindObjectOfType<GestorDeRede>()==null){
            Instantiate(Resources.Load("GestorDeRede"), Vector3.zero, Quaternion.identity);
        }
    }
}
