using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal bool gameActive = false;
    
    [SerializeField] Transform playerWhiteSpawnPointRef;
    [SerializeField] Transform playerBlackSpawnPointRef;

    


    [SerializeField] private GameObject playerPrefab;


    [Header("Readonly")]
    [ReadOnly] [SerializeField] GameObject playerWhite;
    [ReadOnly] [SerializeField] GameObject playerBlack;

    [ReadOnly][SerializeField] Vector2 playerBlackSpawnVector;
    [ReadOnly][SerializeField] Vector2 playerWhiteSpawnVector;
    

    // Start is called before the first frame update
    void Start()
    {
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

        playerWhite.name = "Player White";
        playerBlack.name = "Player Black";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameLoop(){
        if(gameActive){
            SpawnPlayers();
        }
    }

    public void SpawnPlayers(){

    }
}
