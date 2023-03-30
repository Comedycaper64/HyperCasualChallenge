using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransientText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float decaySpeed;

    private void Awake() 
    {
        Destroy(gameObject, 1f);    
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }

    private void Update() 
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, scoreText.color.a - (decaySpeed * Time.deltaTime));
    }
}
