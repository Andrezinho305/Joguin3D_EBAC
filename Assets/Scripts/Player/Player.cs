using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class Player : Singleton<Player>//, IDamageable
{
    public CharacterController characterController;
    public List<Collider> colliders;
 
    [Header("Movement")]
    public float speed = 1f;
    public float turnSpeed = 1f;

    [Header("Jump")]
    public float jumpSpeed = 15f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;
    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Runing")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;


    [Header("Animation")]
    public Animator animator;

    [Header("Flash")]
    public List<FlashColour> flashColor;

    [Header("Life")]
    public HealthBase healthBase;

    private bool _alive = true;


    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }



    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0); //gira o personagem no eixo horizontal default da unity inputado

        var inputAxisVertical = Input.GetAxis("Vertical"); //define como barra de espaço ou default de movimento vertical da unity
        var speedVector = transform.forward * inputAxisVertical * speed; //conta para o input de velocidade

        if (characterController.isGrounded) //handler do pulo
        {
            vSpeed = 0; //
            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed; //define velocidade vertical como 0
            }
        }

        vSpeed -= gravity * Time.deltaTime; //reduz a velocidade vertical pela gravidade constantemente
        speedVector.y = vSpeed; //faz com que o personagem caia com a velocidade 


        var isWalking = inputAxisVertical != 0; // identifica se a velocidade de movimento é 0 ou não

        if (isWalking) //se for diferente de 0 permite rodar a função
        {
            if (Input.GetKey(keyRun)) //get key = enquanto segura
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }


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



    #region Life & Damage Taken

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    public void Damage(HealthBase h)
    {
        flashColor.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        CameraShake.Instance.Shake();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;

            animator.SetTrigger("Death");

            colliders.ForEach(i => i.enabled = false); //desativa todos os coliders dentro da lista

            Invoke(nameof(Revive), 3f); //puxa o code de revive após 3 segundos
        }
    }

    private void Revive()
    {
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        _alive = true;
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);

    }


    #endregion

    #region Respawn System

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.LastCheckpointPosition();
        }
    }

    #endregion



}


