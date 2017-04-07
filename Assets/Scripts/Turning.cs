using UnityEngine;
using System.Collections;

public class Turning : MonoBehaviour {

    public float turnEnabledTime = 1; // Turntime -> turnEnabledTime
    public float touchSwipeSpeed = 10f; // touchSpeed -> touchSwipeSpeed
    public float touchVerticalSwipeSpeed; //touchSpeedVertical -> touchVerticalSwipeSpeed
    //public float touchdeltaposition = 50f; // 안쓰이므로 삭제
    
    public bool Win = false;

    Vector2 touchStartPosition; // startPos -> touchStartPosition
    Vector2 changedMovementDirection; // direction -> changedMovementDirection
    bool isMovementDirectionChosen; // directionChosen -> isMovementDirectionChosen

    bool rightTurnEnabled = false; //verright -> rightTurnEnabled
    bool leftTurnEnabled = false; // verleft -> leftTurnEnabled

    float i = 0f;

    void Update()
    {


        bool r = Input.GetKeyDown(KeyCode.RightArrow);
        bool l = Input.GetKeyDown(KeyCode.LeftArrow);

        if (r)
        {
            rightTurnEnabled = true;
        }
        else if (l)
        {
            leftTurnEnabled = true;
        }

        if (Input.touchCount >0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    isMovementDirectionChosen = false;
                    break;
                case TouchPhase.Moved:
                    changedMovementDirection = touch.position - touchStartPosition;
                    break;
                case TouchPhase.Ended:
                    isMovementDirectionChosen = true;
                    break;
            }
        }
        if (isMovementDirectionChosen)
        {
            if(changedMovementDirection.x > touchSwipeSpeed)
            {
                rightTurnEnabled = true;
                isMovementDirectionChosen = false;
            }
            else if (changedMovementDirection.x < -touchSwipeSpeed)
            {
                leftTurnEnabled = true;
                isMovementDirectionChosen = false;
            }
            else if (changedMovementDirection.y > touchVerticalSwipeSpeed)
            {
                GetComponentInParent<PlayerController>().anim.SetTrigger("Jump");
                StartCoroutine(GetComponentInParent<PlayerController>().DisableLowerBodyCollider());
                isMovementDirectionChosen = false;
            }
            else if (changedMovementDirection.y < -touchVerticalSwipeSpeed)
            {
                GetComponentInParent<PlayerController>().anim.SetTrigger("Slide");
                StartCoroutine(GetComponentInParent<PlayerController>().DisableUpperBodyCollider());
                isMovementDirectionChosen = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (rightTurnEnabled || leftTurnEnabled)
        {
            i = i + Time.deltaTime;
            if (turnEnabledTime -i < 0.05)
            {
                i = 0f;
                rightTurnEnabled = false;
                leftTurnEnabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Turn" && i != 0)
        {
            GetComponentInParent<PlayerController>().Turning(rightTurnEnabled, leftTurnEnabled);
            i = 0f;
            leftTurnEnabled = false;
            rightTurnEnabled = false;
        }
        if(other.tag == "Win")
        {
            Win = true;
            GetComponentInParent<Animator>().SetTrigger("Win");
            GetComponentInParent<PlayerController>().speed = 0;
        }
    }
}
