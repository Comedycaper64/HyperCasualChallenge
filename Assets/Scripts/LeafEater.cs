using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafEater : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Transform nearestLeaf;
    [SerializeField] private LeafSpawner spawner;

    private void Update() 
    {
        if (!GameStartManager.isGameStarted) {return;}

        if ((nearestLeaf != null) && (nearestLeaf.gameObject.activeInHierarchy))
        {
            transform.LookAt(nearestLeaf.transform, Vector3.back);
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            transform.position = Vector3.MoveTowards(transform.position, nearestLeaf.transform.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            PickNextLeaf();
        }
    }

    private void PickNextLeaf()
    {
        foreach(GameObject leaf in spawner.leafSpool)
        {
            if (leaf.activeInHierarchy)
            {
                nearestLeaf = leaf.GetComponent<Leaf>().GetSpriteTransform();
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Leaf leaf = other.GetComponentInParent<Leaf>();
        leaf.gameObject.SetActive(false);
        ScoreManager.Instance.RemoveFromScore(leaf.GetLeafScore());
        ScoreManager.Instance.SpawnTransientText(other.transform.position, leaf.GetLeafScore().ToString(), false);
        PickNextLeaf();
    }
}
