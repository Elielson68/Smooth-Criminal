using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HordarController : MonoBehaviourPunCallbacks
{
    public static HordarController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        CriarInimigo();
    }

    public void CriarInimigo(){
        float maxZ = 4.17f;
        float minZ = -1.09f;
        float minX = 5.34f;
        float maxX = 8.88f;
        float z = Random.Range(minZ, maxZ);
        float x = Random.Range(minX, maxX);
        Vector3 posiSpawn = new Vector3(x, 0.1158979f, z);
        bool teste = PhotonNetwork.PrecisionForFloatSynchronization.AlmostEquals(1f, 2f);
       PhotonNetwork.InstantiateRoomObject("Inimigo2 Variant", posiSpawn, Quaternion.identity);
    }
}
