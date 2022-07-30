using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float speed = 20f;
    public float touchSpeed = 1f;
    Animator anim;

    Vector3 firstPos, endPos;
    private float offSetX;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (GameManager.instance.testMode == true)
        {
            speed = 40;
            GameManager.instance.PC = true;
        }
        else
        {
            speed = 20;
        }
    }

    void Update()
    {
        if (GameManager.instance.gameOver == false && GameManager.instance.Android == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                endPos = Input.mousePosition;
                offSetX = endPos.x - firstPos.x;
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                firstPos = Vector3.zero;
                endPos = Vector3.zero;
            }
            transform.Translate(offSetX * Time.deltaTime * touchSpeed / 100, 0, speed * Time.deltaTime);
        }
        else if(GameManager.instance.gameOver == true && GameManager.instance.Android == true)
        {
            anim.SetBool("Idle", true);
            speed = 0;
        }


        if (GameManager.instance.gameOver == false && GameManager.instance.PC == true)
        {
            var hor = Input.GetAxis("Horizontal");
            transform.Translate(hor * speed * Time.deltaTime, 0f, speed * Time.deltaTime);
        }
        else if(GameManager.instance.gameOver == true && GameManager.instance.PC == true)
        {
            anim.SetBool("Idle", true);
            speed = 0;
        }


        if (GameManager.instance.dance == true)
        {
            anim.SetBool("Dance", true);
        }
    }
}
