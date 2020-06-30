using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TackCtrl : MonoBehaviourPunCallbacks
{

    private Transform tr;
    private Rigidbody rb;

    //이동속도
    public float moveSpeed = 10.0f;
    //회전속도
    public float turnSpeed = 100.0f;

    //포탄 프리팹
    public GameObject cannonObj;//포탄 프리팹
    public Transform firePos;   //포탄의 발사 위치

    private PhotonView pv;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        
        /*
        if (!pv.IsMine) this.enabled = false;

        this.enabled = pv.IsMine;
        */

        rb.centerOfMass = new Vector3(0.0f, -5.0f, 0.0f);
    }

    void Update()
    {
        if (pv.IsMine)
        {   
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            tr.Translate(Vector3.forward * Time.deltaTime * moveSpeed * v);
            tr.Rotate(Vector3.up * Time.deltaTime * turnSpeed * h);

            if (Input.GetMouseButtonDown(0))
            {
                pv.RPC("Fire", RpcTarget.AllViaServer);
            }
        }
    }

    [PunRPC]
    void Fire()
    {
        GameObject cannon = Instantiate(cannonObj, firePos.position, firePos.rotation);
        cannon.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 5000.0f);
    }
}
