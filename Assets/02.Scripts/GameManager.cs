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
        Vector3 pos = new Vector3( Random.Range(-200, 200)
                                    , 5.0f
                                    , Random.Range(-200, 200));

        PhotonNetwork.Instantiate("Tank", pos, Quaternion.identity, 0);
    }
}
