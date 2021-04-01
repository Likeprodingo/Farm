using System;
using Assets.Scripts.Enum;
using GamePlay.AnimationState.Impl;
using UnityEngine;

namespace GamePlay.Enviroment.TIleStates
{
    public abstract class TileState
    {
        public static event Action<TileState> StateCompleted = delegate {  };
        public static event Action<TileState> EnteredState = delegate {  };

        protected PlayerState _playerMoveState = PlayerState.WALK;

        public PlayerState PlayerMoveState => _playerMoveState;

        public Material Material { get; } = default;

        public Action StateAction { get; } = default;

        protected TileState(Material material, Action action)
        {
            Material = material;
            StateAction = action;
        }

        public virtual void Process()
        {
            EnteredState.Invoke(this);
        }

        public void CompleteState()
        {
            StateCompleted.Invoke(this);
        }
    }
}