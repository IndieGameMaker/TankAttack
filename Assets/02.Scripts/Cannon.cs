using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int actNumber;
    
    void OnCollisionEnter(Collision coll)
    {
        Destroy(this.gameObject);
    }
}
