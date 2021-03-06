﻿/*using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Direction currentDir;
    Vector2 input;
    bool isMoving = false;
    Vector3 startPos;
    Vector3 endPos;
    float t;

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    public float walkSpeed = 3f;

    public bool isAllowedToMove = true;

    void Start()
    {
        isAllowedToMove = true;
    }

	void Update () { 

        if(!isMoving && isAllowedToMove)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if(input != Vector2.zero)
            {

                if(input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if(input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if(input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch(currentDir)
                {
                    case Direction.North:
                        gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        break;
                    case Direction.East:
                        gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        break;
                    case Direction.South:
                        gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        break;
                    case Direction.West:
                        gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        break;
                }

                StartCoroutine(Move(transform));
            }

        }

	}

    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

        while (t < 1f)
        {
            t += Time.deltaTime * walkSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }
}

enum Direction
{
    North,
    East,
    South,
    West
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidBod;
    public Vector3 change;
    public Animator Ani;
    public bool canMove;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Ani = GetComponent<Animator>();
        myRigidBod = GetComponent<Rigidbody2D>();
        canMove = true;
      
    }

  
// Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        AnimateMove();
        StopMovement();
    }

    public void StopMovement()
    {
        if (SceneManager.GetSceneByName("Battle_Scene").isLoaded){
            //print("Trouble");
            CanNotMove();
        }
        if (GameObject.FindGameObjectWithTag("MenuCanvas")) {
            //print("Trouble");
            myRigidBod.constraints = RigidbodyConstraints2D.FreezePosition;
            CanNotMove();  
        }
        else {
            myRigidBod.constraints = RigidbodyConstraints2D.FreezeRotation;
            AnimateMove();
        }
    }

  
    void AnimateMove()
    {
        if (canMove && change != Vector3.zero)
        {
            Move();
            Ani.SetFloat("inputX", change.x);
            Ani.SetFloat("inputY", change.y);
            Ani.SetBool("moving", true);
        }
        else
        {
            Ani.SetBool("moving", false);
        }
    }
    void Move()
    {
        
        myRigidBod.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
        
    }
    void CanNotMove() {
        Ani.SetFloat("inputX", 0);
        Ani.SetFloat("inputY", 0);
        Ani.SetBool("moving", false);
    }

}


