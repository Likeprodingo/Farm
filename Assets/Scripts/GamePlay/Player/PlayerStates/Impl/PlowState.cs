using Assets.Scripts.Enum;
using DefaultNamespace;
using GamePlay.PlayerActions;
using UnityEngine;

namespace GamePlay.AnimationState.Impl
{
    public class PlowState : MoveState
    {
        public PlowState(GameObject gameObject) : base(gameObject)
        {
            playerState = PlayerState.PLOW;
        }
    }
}