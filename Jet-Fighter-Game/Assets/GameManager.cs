using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace MJDeveloping.Games.Unity.JetFighter{

    public class GameManager : MonoBehaviour
    {
        public static readonly string PLAYER_BLACK_NAME = "Player Black";
        public static readonly string PLAYER_WHITE_NAME = "Player White";
        
        

        [Range(60,180)]
        [SerializeField] int totalGameTime;
        [SerializeField] [ReadOnly] float timeRemaining;
        [SerializeField] int debugGameplayAccelerator;

        
        [Header("Manager Script Reference")]
        public ScoreManager ScoreManager;
        [Header("State Scripts")]
        public GameComplete GameComplete;
        internal bool gameActive = false;
        
        [SerializeField] Transform playerWhiteSpawnPointRef;
        [SerializeField] Transform playerBlackSpawnPointRef;

        


        [SerializeField] private GameObject playerPrefab;


        [Header("Readonly")]
        [ReadOnly] [SerializeField] GameObject playerWhite;
        [ReadOnly] [SerializeField] GameObject playerBlack;

        [ReadOnly][SerializeField] Vector2 playerBlackSpawnVector;
        [ReadOnly][SerializeField] Vector2 playerWhiteSpawnVector;   

        [SerializeField] public GameObject projectileParent;

        [SerializeField] List<GameObject> stateCollection;

        static GameManager instance;

        public static GameManager Instance { get => instance;}

        [SerializeField] GAME_STATE currentState;
        [SerializeField] GAME_RESULT result;
        
        private  void Awake() {            
            currentState = GAME_STATE.Init;
            instance = gameObject.GetComponent<GameManager>();                
        }
        
        public void StartGame(){
            
            currentState = GAME_STATE.Game;

        }

        void SetupGame(){
            playerWhiteSpawnVector = playerWhiteSpawnPointRef.position;
            playerBlackSpawnVector = playerBlackSpawnPointRef.position;

            // print(playerBlackSpawnVector);
            // print(playerWhiteSpawnVector);

            playerWhite = Instantiate(playerPrefab, playerWhiteSpawnVector,Quaternion.identity);
            playerBlack = Instantiate(playerPrefab, playerBlackSpawnVector,Quaternion.identity);

            playerWhite.GetComponent<SpriteRenderer>().color = Color.white;
            playerBlack.GetComponent<SpriteRenderer>().color = Color.black;

            playerWhite.GetComponent<PlayerController>().SetPlayerColour(Color.white);// = Color.white;
            playerBlack.GetComponent<PlayerController>().SetPlayerColour(Color.black);//.color = Color.black;

            playerWhite.name = PLAYER_WHITE_NAME;
            playerBlack.name = PLAYER_BLACK_NAME;

            gameActive = true;
        }        

        // Update is called once per frame
        void Update()
        {            
            print(stateCollection.Count);
            GameLoop();   
        }

        private void HideAll(){
            foreach (GameObject parent in stateCollection)
                    {
                        parent.gameObject.SetActive(false);                        
                    }
        }

        private void CheckState()
        {
            switch(currentState){
                case GAME_STATE.Init:
                    HideAll();
                    stateCollection[0].SetActive(true);
                    break;
                case GAME_STATE.Game:
                    //TODO
                    if(!gameActive){
                        HideAll();
                        stateCollection[1].SetActive(true);
                        SetupGame();    
                    }                
                    break;
                case GAME_STATE.Completed:
                    //TODO
                    HideAll();
                    stateCollection[2].SetActive(true);
                    break;
            }
        }

        public void GameLoop(){
            CheckState();
            if(currentState == GAME_STATE.Game){
                SpawnPlayers();
                print(Time.deltaTime * debugGameplayAccelerator);
                timeRemaining = (totalGameTime/debugGameplayAccelerator) - Time.fixedDeltaTime;
                if(timeRemaining <= 0)
                {
                    gameActive = false;
                    GameOver();
                }                
            }
            
        }

        private void GameOver()
        {
            currentState = GAME_STATE.Completed;
            if(ScoreManager.PlayerBlackScore > ScoreManager.PlayerWhiteScore){
                result = GAME_RESULT.PlayerBlackWins;
            } else if(ScoreManager.PlayerBlackScore == ScoreManager.PlayerWhiteScore){
                result = GAME_RESULT.Draw;
            } else{
                result = GAME_RESULT.PlayerWhiteWins;
            }
            GameComplete.ResultTextFormer(result, ScoreManager.PlayerBlackScore, ScoreManager.PlayerWhiteScore);
        }

        public void SpawnPlayers(){

        }
    }
}

public enum GAME_STATE{
    Init,
    Game,
    Completed
}

public enum GAME_RESULT{
    PlayerWhiteWins,
    Draw,
    PlayerBlackWins

}