using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 100f;

    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    Animator anim;
    Rigidbody rigid;

    AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(0f, 0f, vertical);  // Solo movimiento adelante/atrás, sacamos el horizontal, asi con ese eje giramos al personaje
        movement.Normalize();

        bool isWalking = !Mathf.Approximately(vertical, 0f);
        anim.SetBool("IsWalking", isWalking);

        // Rotación solo con horizontal
        transform.Rotate(0f, horizontal * turnSpeed * Time.deltaTime, 0f);

        if (isWalking)
        {
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }else
        {
            audio.Stop();
        }
    }

    // Update is called once per frame
    //void Update()  //codigo antiguo
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    movement.Set(horizontal, 0f, vertical);
    //    movement.Normalize();

    //    bool horizontalInput = !Mathf.Approximately(horizontal, 0f);
    //    bool verticalInput = !Mathf.Approximately(vertical, 0f);

    //    bool isWalking = horizontalInput || verticalInput;
    //    anim.SetBool("IsWalking", isWalking);

    //    Vector3 desireForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
    //    rotation = Quaternion.LookRotation(desireForward);
    //}

    private void OnAnimatorMove()
    {
        //rigid.MovePosition(rigid.position + movement * anim.deltaPosition.magnitude); //esto hacia que mi movimiento fuera con cordenadas globales
        //con el siguiente codigo pasa a ser de manera local

        Vector3 localMovement = transform.TransformDirection(movement); //codigo nuevo
        rigid.MovePosition(rigid.position +  localMovement*anim.deltaPosition.magnitude); //codigo nuevo 
        //tuve que subir el turn speed de 20f a 100f 

        //rigid.MoveRotation(rotation); //codigo antiguo
    }
}
