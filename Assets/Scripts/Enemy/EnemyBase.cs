using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColour flashColor;

        public ParticleSystem particleSystem;

        public float startLife = 10f;



        [SerializeField] private float _currentLife;

        [Header("Spawn Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;

        [Header("Animations")]
        [SerializeField] private AnimationBase _animationBase;


        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithSpawnAnimation)
                SpawnAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
            
        }



        protected virtual void Kill()
        {


            OnKill();
        }

        protected virtual void OnKill()
        {
            if (collider != null) collider.enabled = false;


            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);


        }

        public void OnDamageTaken(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(15);

            _currentLife -= f;
            
            if(_currentLife <= 0)
            {
                Kill();
            }           
        }

        public void Damage (float damage) //interface usada para fazer checagens em larga escala
        {
            Debug.Log("Damaged");
            OnDamageTaken(damage);

        }


        #region Animations

        private void SpawnAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }


        #endregion

        #region Debug


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) OnDamageTaken(5f);
        }




        #endregion

    }
}
