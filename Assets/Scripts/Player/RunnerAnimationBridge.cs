using UnityEngine;

namespace RunMarrakech.Player
{
    public sealed class RunnerAnimationBridge : MonoBehaviour
    {
        public Animator Animator;
        public CharacterController Controller;
        public string SpeedParameter="Speed";
        public string GroundedParameter="Grounded";
        public string SlideTrigger="Slide";
        public string HitTrigger="Hit";

        void Awake()
        {
            if(!Animator) Animator=GetComponentInChildren<Animator>();
            if(!Controller) Controller=GetComponent<CharacterController>();
        }

        void Update()
        {
            if(!Animator) return;
            float speed=RunMarrakech.Core.GameManager.Instance ? RunMarrakech.Core.GameManager.Instance.RunSpeed : 0f;
            Animator.SetFloat(SpeedParameter,speed);
            Animator.SetBool(GroundedParameter,Controller && Controller.isGrounded);
        }

        public void PlaySlide(){if(Animator)Animator.SetTrigger(SlideTrigger);}
        public void PlayHit(){if(Animator)Animator.SetTrigger(HitTrigger);}
    }
}