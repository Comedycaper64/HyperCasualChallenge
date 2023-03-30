using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Rigidbody2D>())
        {
            Leaf leaf = other.GetComponentInParent<Leaf>();
            leaf.gameObject.SetActive(false);
        }    
    }
}
