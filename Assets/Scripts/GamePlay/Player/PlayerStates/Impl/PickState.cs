using Assets.Scripts.Enum;
using DefaultNamespace;
using UnityEngine;

namespace GamePlay.AnimationState.Impl
{
    public class PickState : MoveState
    {
        public PickState(GameObject gameObject) : base(gameObject)
        {
            playerState = PlayerState.PICK;
        }
    }
}