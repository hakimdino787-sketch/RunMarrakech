using UnityEngine;
namespace RunMarrakech.Player
{
    public sealed class RunnerJumpController : MonoBehaviour
    {
        public CharacterController Controller;
        public float JumpForce = 8.5f;
        public float Gravity = -24f;
        float yVelocity;
        void Awake(){if(!Controller) Controller=GetComponent<CharacterController>();}
        public void Jump(){if(Controller && Controller.isGrounded) yVelocity=JumpForce;}
        void Update(){if(!Controller)return;if(Controller.isGrounded && yVelocity<0)yVelocity=-2f;yVelocity+=Gravity*Time.deltaTime;Controller.Move(Vector3.up*yVelocity*Time.deltaTime);}
    }
}