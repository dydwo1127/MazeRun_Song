using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour
{
    public float turnTime;

    bool verright = false;
    bool verleft = false;

    Rigidbody playerRigidbody;

    public GameObject turnCollider;

    void FixedUpdate ()
    {
        bool r = Input.GetKeyDown(KeyCode.RightArrow);
        bool l = Input.GetKeyDown(KeyCode.LeftArrow);

        /*if (r)
        {
            verright = true;
        }
        else if (l)
        {
            verleft = true;
        }

        else
        {
            verright = false;
            verleft = false;
        }*/

        //Turning(verright, verleft);
        Turning(r,l);
    }

    void Turning(bool verright, bool verleft)
    {
        if (verright)
        {
            playerRigidbody.transform.Rotate(0f, 90f, 0f);
        }
        else if (verleft)
        {
            playerRigidbody.transform.Rotate(0f, -90f, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turn")
        {
            Debug.Log("aa");
        }
    }
}
