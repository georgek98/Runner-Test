using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
public class CharacterRotate : MonoBehaviour
{
   private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20f;
    private CharacterController controller;
    public float JumpSpeed = 8.0f;
    public float Speed = 6.0f;

    bool isInSwipeArea;


    InputManagment inputDetector = null;

    // Use this for initialization
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

        if (GameManager.Instance.CanSwipe && inputDirection.HasValue &&
         controller.isGrounded && inputDirection == InputDirection.Right)
        {
            transform.Rotate(0, 90, 0);
            moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
            //allow the user to swipe once per swipe location
            GameManager.Instance.CanSwipe = false;
        }
        else if (GameManager.Instance.CanSwipe && inputDirection.HasValue &&
         controller.isGrounded && inputDirection == InputDirection.Left)
        {
            transform.Rotate(0, -90, 0);
            moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
            GameManager.Instance.CanSwipe = false;
        }


    }

}
