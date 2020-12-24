using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager myGM;
    [SerializeField] int movementSpeed;
    [SerializeField] Color playerColour;
    [SerializeField] GameObject projectilePrefab;

    [Header("Readonly")]
    [ReadOnly][SerializeField] Rigidbody2D myRB;
    [ReadOnly] [SerializeField] int horizontalMovementInput;
    [ReadOnly] [SerializeField] int verticalMovementInput;
    [ReadOnly] [SerializeField] float horizontalRotationInput;
    [ReadOnly] [SerializeField] float verticalRotationInput;
    [ReadOnly] [SerializeField] float zAxisRotationInput;

    [Header("Facing Angles")]
    [SerializeReference] COMPASS compass;
    
    

    public void SetPlayerColour (Color colour){
        playerColour = colour;
    }
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
        ApplyMovement();       
        //ApplyRotation();                        
        if(zAxisRotationInput != 0){
        print(zAxisRotationInput);
        }

        if(Keyboard.current.tabKey.wasPressedThisFrame){
            print("Current Rotation is: "+ transform.rotation);
        }
    }

    void CheckForInput(){


        if(playerColour == Color.black){

            if(Keyboard.current.wKey.isPressed){
                verticalMovementInput = 1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.North);
                
            } else if(Keyboard.current.sKey.isPressed){
                verticalMovementInput = -1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.South);    

            } else{
                verticalMovementInput = 0;                
            }
            
            if(Keyboard.current.dKey.isPressed){
                horizontalMovementInput = 1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.East);    
            }else if(Keyboard.current.aKey.isPressed){
                horizontalMovementInput = -1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.West);    
            }else{
                horizontalMovementInput = 0;                
            }        

            if(Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.North_West);    
            }
            if(Keyboard.current.wKey.isPressed && Keyboard.current.dKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.North_East);    
            }
            if(Keyboard.current.sKey.isPressed && Keyboard.current.aKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.South_West);    
            }
            if(Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.South_East);    
            }
            if(Keyboard.current.spaceKey.wasPressedThisFrame){
                Shoot();
            }    
            //print("Player colour: "+playerColour+". Horizontal Input: "+horizontalMovementInput +"\nVertical Input: "+verticalMovementInput);    
        }

        if(playerColour == Color.white){

            if(Keyboard.current.upArrowKey.isPressed){
                verticalMovementInput = 1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.North);
                
            } else if(Keyboard.current.downArrowKey.isPressed){
                verticalMovementInput = -1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.South);    

            } else{
                verticalMovementInput = 0;                
            }
            
            if(Keyboard.current.rightArrowKey.isPressed){
                horizontalMovementInput = 1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.East);    
            }else if(Keyboard.current.leftArrowKey.isPressed){
                horizontalMovementInput = -1;
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.West);    
            }else{
                horizontalMovementInput = 0;                
            }        

            if(Keyboard.current.upArrowKey.isPressed && Keyboard.current.leftArrowKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.North_West);    
            }
            if(Keyboard.current.upArrowKey.isPressed && Keyboard.current.rightArrowKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.North_East);    
            }
            if(Keyboard.current.downArrowKey.isPressed && Keyboard.current.leftArrowKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.South_West);    
            }
            if(Keyboard.current.downArrowKey.isPressed && Keyboard.current.rightArrowKey.isPressed){
                transform.rotation = Quaternion.Euler(0,0,(float)COMPASS.South_East);    
            }
            if(Keyboard.current.enterKey.wasPressedThisFrame){
                Shoot();
            }    
            //print("Player colour: "+playerColour+". Horizontal Input: "+horizontalMovementInput +"\nVertical Input: "+verticalMovementInput);    
        }
            }

    void Shoot(){
        
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<SpriteRenderer>().color = playerColour;
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        // if(myRB.velocity.x == 0 && myRB.velocity.y == 0){
        //     Debug.Log("x and y is 0");
        //     if(myRB.velocity.x == 0){
        //         myRB.velocity = new Vector2(myRB.velocity.x+1, myRB.velocity.y);
        //     }                    
        // }
        //    projectileRB.velocity = myRB.velocity * 5;
        // print(gameObject.name + " Tried to fire a shot.");
        projectileRB.velocity = transform.right * 5;

    }

    void ApplyMovement(){
        myRB.velocity = new Vector2(horizontalMovementInput,verticalMovementInput) * Time.fixedDeltaTime * movementSpeed;
        // Transform oldRotation = myRB.transform;
        // myRB.transform.rotation = new Quaternion(horizontalMovementInput, verticalMovementInput, 0, 0);
        // oldRotation = myRB.transform;

        // print("My RB Rotation: "+myRB.transform.rotation);
        // print("My old RB Rotation: "+oldRotation.rotation);
    }

    // void ApplyRotation(){
    //     //gameObject.transform.rotation = new Quaternion(0,0, zAxisRotationInput, 0); //gameObject.transform.rotation.w);
    //     transform.rotation = Quaternion.Euler(0,0,zAxisRotationInput);
    //    // gameObject.transform.rotation.SetEulerRotation(0,0,90);
    //     print(gameObject.transform.rotation);
    // }
    
}

public enum COMPASS{
    North = 90,    
    East = 0,
    South = -90,
    West = 180,
    North_East = 45,
    North_West = 135,
    South_East = -45,
    South_West = -135
}
