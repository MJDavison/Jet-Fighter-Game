using System;
using System.Collections;
using System.Collections.Generic;
using MJDeveloping.Games.Unity.JetFighter;
using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{
    GameManager myGM; 
    [SerializeField] Text resultText;
    private string resultString;

    private void Awake() {
        myGM = GameManager.Instance;
    }    

    public void ResultTextFormer(GAME_RESULT result, int playerBlackScore, int playerWhiteScore)
    {
        string resultToString = "";
        switch(result){
            case GAME_RESULT.PlayerBlackWins:
            resultToString += "Player Black Wins!";
            break;

            case GAME_RESULT.PlayerWhiteWins:
            resultToString += "Player White Wins!";
            break;

            case GAME_RESULT.Draw:
            resultToString += "It's a draw!";
            break;
        }
        resultString = "GAME OVER! \n Final Result: " +playerBlackScore +" : " +playerWhiteScore + "\n "+ resultToString;

        UpdateText();
    }

    private void UpdateText()
    {
        resultText.text = resultString;
    }
}
