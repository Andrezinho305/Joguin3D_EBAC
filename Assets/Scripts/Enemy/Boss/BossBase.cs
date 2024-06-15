using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;
using DG.Tweening;


namespace Boss
{
    public enum BossActions
    {
        INIT,
        IDLE,
        ATTACK,
        WALK,
        DEATH


    }



    public class BossBase : MonoBehaviour
    {
        private StateMachine<BossActions> stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Movement")]
        public float bossSpeed = 5f;
        public List<Transform> waypoints;
        private float _minDistance = 1f;

        [Header("Attack")]
        public int attackAmmo = 5;
        public float timeBetweenAttacks = .5f;

        public bool lookAtPlayer = false;
        private Player _player;

        [Header("Health")]
        public HealthBase healthBase;

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }


        private void Init()
        {
            stateMachine = new StateMachine<BossActions>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossActions.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossActions.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossActions.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossActions.DEATH, new BossStateDeath());

        }


        private void OnBossKill(HealthBase h)
        {
           SwitchStates(BossActions.DEATH);

        }


        #region Movement
        public void GoToRandomPoint(Action onArrive = null) //o = nulll significa que o parametro não é obrigatório
        {
            StartCoroutine(GoToPointCourroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCourroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position)> _minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * bossSpeed);
                yield return new WaitForEndOfFrame();
            }

            onArrive?.Invoke(); // if onArrive != null onArrive.Invoke

        }

        #endregion


        #region Attack

        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(StartAttackCorroutine(endCallback));
        }

        IEnumerator StartAttackCorroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmmo)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);

            }

            endCallback?.Invoke();

        }

        #endregion


        #region Animations
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();

        }







        #endregion



        #region Debug

        [NaughtyAttributes.Button]
        private void SwitchInit()

        {
            SwitchStates(BossActions.INIT);
        }        
        
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchStates(BossActions.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchStates(BossActions.ATTACK);
        }


        #endregion


        private void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }



        #region State Machine

        public void SwitchStates(BossActions state)
        {
            stateMachine.SwitchState(state, this); //params le dentro dessa lista a partir da variavel ex.:(state, this, transform, gameobject, this)
        }


        #endregion

       

    }
}

