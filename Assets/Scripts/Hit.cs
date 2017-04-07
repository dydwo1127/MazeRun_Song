using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

    Animator anim;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacles")
        {
            anim.SetTrigger("Hit");
            GetComponentInParent<PlayerController>().speed = 0f;
        }
    }
}
