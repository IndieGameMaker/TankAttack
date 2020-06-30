using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    void OnCollisionEnter(Collision coll)
    {
        Destroy(this.gameObject);
    }
}
