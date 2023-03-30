using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberHead : MonoBehaviour
{
    private ColourType colourType;
    [SerializeField] private Collider2D headCollider;

    private void Awake() 
    {
        ToggleCollider(false);    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Rigidbody2D>())
        {
            Leaf leaf = other.GetComponentInParent<Leaf>();
            leaf.gameObject.SetActive(false);
            int scoreToAdd = leaf.GetLeafScore();
            if (leaf.GetLeafColour() == colourType)
            {
                scoreToAdd *= 2;
            }
            ScoreManager.Instance.AddToScore(scoreToAdd);
            ScoreManager.Instance.SpawnTransientText(other.transform.position, scoreToAdd.ToString(), true);
        }   
    }

    public void ToggleCollider(bool enable)
    {
        headCollider.enabled = enable;
    }

    public void SetColourType(ColourType colourType)
    {
        this.colourType = colourType;
    }
}
