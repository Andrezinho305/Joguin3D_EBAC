using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController; 
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    private float vSpeed = 0f;

    [Header("Animation")]
    public Animator animator;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0); //gira o personagem no eixo horizontal inputado

        var inputAxisVertical = Input.GetAxis("Vertical"); //pulo
        var speedVector = transform.forward * inputAxisVertical * speed; //conta para o input de velocidade

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed; //faz com que o personagem caia com a velocidade 

        characterController.Move(speedVector * Time.deltaTime); //uso do controlador para se mexer



        animator.SetBool("Run", inputAxisVertical != 0); //se a relação eh true, define a bool como true
        //funciona igual aos ifs
        /*if (inputAxisVertical != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }*/

    }



}
