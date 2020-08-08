using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int actNumber;
    public Rigidbody rb;
    public Transform tr;
    public BoxCollider bc;
    public SphereCollider sc;
    public Transform tr2;
    
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
    }
    
    void OnCollisionEnter(Collision coll)
    {
        Destroy(this.gameObject);
    }
}
