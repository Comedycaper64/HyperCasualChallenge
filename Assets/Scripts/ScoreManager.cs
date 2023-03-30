using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject transientText;

    private void Awake() 
    {
        Instance = this;    
        UpdateScoreText();
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreText();
    }

    public void SpawnTransientText(Vector3 position, string score)
    {
        TransientText transient = Instantiate(transientText, this.transform).GetComponent<TransientText>();
        transient.SetScoreText(score);
        transient.transform.position = position;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
