using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class TackCtrl : MonoBehaviourPunCallbacks
{
    private CinemachineVirtualCamera cvc;
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
    //Health
    private float currHp = 100.0f;
    private float initHp = 100.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();

        if (pv.IsMine)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("V_CAM");
            cvc = obj.GetComponent<CinemachineVirtualCamera>();

            cvc.Follow = tr;
            cvc.LookAt = tr;
        }
        
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
                int actNumber = pv.Owner.ActorNumber;
                pv.RPC("Fire", RpcTarget.AllViaServer, actNumber);
            }
        }
    }

    [PunRPC]
    void Fire(int  _actNumber)
    {
        GameObject cannon = Instantiate(cannonObj, firePos.position, firePos.rotation);
        cannon.GetComponent<Cannon>().actNumber = _actNumber;
        cannon.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 5000.0f);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("CANNON"))
        {
            currHp -= 10.0f;
            if (currHp <= 0.0f)
            {
                DisplayHitInfo(coll.collider.GetComponent<Cannon>().actNumber);
                YouDie();
            }
        }
    }

    void DisplayHitInfo(int _actNumber)
    {
        string userName = "";
        
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            if (player.ActorNumber == _actNumber)
            {
                userName = player.NickName;
            }
        }

        string msg = $"[{PhotonNetwork.NickName}] is killed by [{userName}].";
        GameManager.instance.pv.RPC("SendMsg", RpcTarget.AllBufferedViaServer, msg);
    }


    void YouDie()
    {
        Debug.Log("You Die");
        SetVisible(false);

        Invoke("RespawnTank", 3.0f);
    }

    void RespawnTank()
    {
        //HP 초기화
        currHp = initHp;

        Vector3 pos = new Vector3( Random.Range(-100, 100)
                            , 10.0f
                            , Random.Range(-100, 100));
        tr.position = pos;
        SetVisible(true);
    }

    void SetVisible(bool isVisible)
    {
        foreach (Renderer _render in GetComponentsInChildren<MeshRenderer>())
        {
            _render.enabled = isVisible;
        }
    }
}
