using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //public float fSpeed; //fSpeed -> 삭제
    public float colliderDisableTime; //collidertime -> colliderDisableTime
    //public float turnTime; //turnTime -> 쓰이지 않으므로 삭제
    public float touchspeed;
    
    //Vector3 forward; // forward를 transform.Forward로 대체
    Vector3 movement;
    public Animator anim;
    Rigidbody playerRigidbody;
    float a = 0f;
    float b = 0f;

    public GameObject UpperBodyCollider; //UPcollider -> UpperBodyCollider
    public GameObject LowerBodyCollider; //Downcollider -> LowerBodyCollider
    public GameObject Turncollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (speed == 0)
        {
            if (Input.GetKey(KeyCode.R) || Input.touchCount > 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        //Animating();
    }



    void FixedUpdate ()
    {
        Move(transform.forward);
    }
    

    void Move (Vector3 forward)
    {
        movement.Set(forward.x, forward.y, forward.z);
        movement = movement * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    public void Turning (bool verright, bool verleft)
    {
        if (verright)
        {
            playerRigidbody.transform.Rotate(0f, 90f, 0f);
        }
        if (verleft)
        {
            playerRigidbody.transform.Rotate(0f, -90f, 0f);
        }
    }

    public IEnumerator DisableUpperBodyCollider() // disabelcolliderUp -> DisableUpperBodyCollider
    {
        UpperBodyCollider.SetActive(false);
        speed = speed / 2;
        yield return new WaitForSeconds(colliderDisableTime / 2);
        speed = speed * 2;
        yield return new WaitForSeconds(colliderDisableTime/2);
        
        UpperBodyCollider.SetActive(true);
    }

    public IEnumerator DisableLowerBodyCollider() // disabelcolliderdown -> DisableLowerBodyCollider
    {
        LowerBodyCollider.SetActive(false);
        //speed = speed / 2;
        yield return new WaitForSeconds(colliderDisableTime);
        //speed = speed * 2;
        LowerBodyCollider.SetActive(true);
    }
}
