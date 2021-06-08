using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorSingletonManagerGame : MonoBehaviour
{
    private void Awake() {
        if(FindObjectOfType<GameManager>()==null){
            Instantiate(Resources.Load("GameManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
