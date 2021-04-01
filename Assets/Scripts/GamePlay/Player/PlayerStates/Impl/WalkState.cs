using Assets.Scripts.Enum;
using DefaultNamespace;
using UnityEngine;

namespace GamePlay.AnimationState.Impl
{
    public class WalkState : MoveState
    {
        public WalkState(GameObject gameObject) : base(gameObject)
        {
            playerState = PlayerState.WALK;
        }

        public override bool Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                UpdateStateInvoke();
            }
            return base.Move(direction);
        }
    }
}