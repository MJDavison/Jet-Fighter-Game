using System.Collections;
using System.Collections.Generic;
using MJDeveloping.Games.Unity.JetFighter;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager myGM;

    [SerializeField] Text playerBlackScoreText;
    [SerializeField] Text playerWhiteScoreText;

    [SerializeField] int playerBlackScore;
    [SerializeField] int playerWhiteScore;

    public int PlayerBlackScore { get => playerBlackScore; set => playerBlackScore = value; }
    public int PlayerWhiteScore { get => playerWhiteScore; set => playerWhiteScore = value; }


    // Start is called before the first frame update
    void Start()
    {
        myGM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        playerBlackScoreText.text = PlayerBlackScore.ToString();
        playerWhiteScoreText.text = playerWhiteScore.ToString();
    }
}
