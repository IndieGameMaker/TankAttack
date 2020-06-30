using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        CreateTank();
    }

    void CreateTank()
    {
        Vector3 pos = new Vector3( Random.Range(-100, 100)
                                    , 10.0f
                                    , Random.Range(-100, 100));

        PhotonNetwork.Instantiate("Tank", pos, Quaternion.identity, 0);
    }
}
