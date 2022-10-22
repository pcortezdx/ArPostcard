using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGiftBox : MonoBehaviour
{
    //Animator Variable
    private Animator giftBoxAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //Get Animator Component
        giftBoxAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(giftBoxAnimator != null)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                giftBoxAnimator.SetTrigger("OpenGift");
            }
        }
    }
}
