using System;
using Assets.Scripts.Enum;
using DefaultNamespace;
using GamePlay.PlayerActions;
using UnityEngine;

namespace GamePlay.AnimationState.Impl
{
    public abstract class MoveState
    {
        public static event Action<MoveState> CompleteState = delegate{  };

        private GameObject _item;
        
        protected PlayerState playerState;
        public PlayerState PlayerState => playerState;   

        protected MoveState(GameObject item)
        {
            _item = item;
        }

        public void OnEnterState()
        {
            if (!ReferenceEquals(_item, null))
            {
                _item.SetActive(true);
            }
        }

        public void OnExitState()
        {
            if (!ReferenceEquals(_item, null))
            {
                _item.SetActive(false);
            }
        }
        

        public virtual bool Move(Vector2 direction)
        {
            return true;
        }
        
        protected void UpdateStateInvoke()
        {
            CompleteState.Invoke(this);
        }
    }
}