using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CharacterLeftRightMovment : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20f;
    private CharacterController controller;
    private bool isChangingLane = false;
    private Vector3 locationAfterChangingLane;
    private Vector3 sidewaysMovementDistance = Vector3.right * 2f;

    public float SideWaysSpeed = 5.0f;
    public float Speed = 6.0f;
    InputManagment inputDetector = null;

    void Start()
    {
        moveDirection = transform.forward;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;

        GameManager.Instance.GameState = GameState.Start;
        inputDetector = GetComponent<InputManagment>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                {
                    var instance = GameManager.Instance;
                    instance.GameState = GameState.Playing;

                }
                break;
            case GameState.Playing:

                CheckHeight();

                DetectSwipeLeftRight();

                //apply gravity
                moveDirection.y -= gravity * Time.deltaTime;

                if (isChangingLane)
                {
                    if (Mathf.Abs(transform.position.x - locationAfterChangingLane.x) < 0.1f)
                    {
                        isChangingLane = false;
                        moveDirection.x = 0;
                    }
                }

                //move the player
                controller.Move(moveDirection * Time.deltaTime);

                break;
            case GameState.Dead:
                if (Input.GetMouseButtonUp(0))
                {
                    //restart
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            default:
                break;
        }

    }

    private void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            GameManager.Instance.Die();
        }
    }

    private void DetectSwipeLeftRight()
    {
        var inputDirection = inputDetector.DetectInputDirection();
        if (controller.isGrounded && inputDirection.HasValue && !isChangingLane)
        {
            isChangingLane = true;

            if (inputDirection == InputDirection.Left)
            {
                locationAfterChangingLane = transform.position - sidewaysMovementDistance;
                moveDirection.x = -SideWaysSpeed;
            }
            else if (inputDirection == InputDirection.Right)
            {
                locationAfterChangingLane = transform.position + sidewaysMovementDistance;
                moveDirection.x = SideWaysSpeed;
            }
        }


    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if we hit the left or right border
        if(hit.gameObject.tag == Constants.WidePathBorderTag)
        {
            isChangingLane = false;
            moveDirection.x = 0;
        }
    }

    
}
