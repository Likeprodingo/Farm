using Assets.Scripts.Enum;
using DefaultNamespace;
using UnityEngine;

namespace GamePlay.AnimationState.Impl
{
    public class IdleState : MoveState
    {
        public IdleState(GameObject gameObject) : base(gameObject)
        {
            playerState = PlayerState.IDLE;
        }

        public override bool Move(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                UpdateStateInvoke();
            }
            return false;
        }
    }
}