using System;
using System.Collections.Generic;
using Assets.Scripts.Enum;
using DefaultNamespace;
using GamePlay.AnimationState.Impl;
using GamePlay.Enviroment.TIleStates;
using GamePlay.PlayerActions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.AnimationState
{
    public class StateManager : MonoBehaviour
    {
        public static event Action<MoveState> StateUpdated = delegate {  };

        [SerializeField] private GameObject _shovel = default;
        
        private MoveState _state;

        private Dictionary<PlayerState, MoveState> _states = new Dictionary<PlayerState, MoveState>();

        private void Start()
        {
            _state = new IdleState(null);
            _states.Add(PlayerState.IDLE, _state);
            _states.Add(PlayerState.WALK, new WalkState(null));
            _states.Add(PlayerState.PLOW, new PlowState(_shovel));
            _states.Add(PlayerState.PICK, new PickState(null));
            StateUpdated.Invoke(_state); 
        }
        
        private void OnEnable()
        {
            MoveState.CompleteState += MoveStateOnCompleteState;
            TileState.EnteredState += TileStateOnEnteredState;
        }

        private void TileStateOnEnteredState(TileState obj)
        {
            UpdateState(obj.PlayerMoveState);
        }
        
        private void OnDisable()
        {
            MoveState.CompleteState -= MoveStateOnCompleteState;
            TileState.EnteredState -= TileStateOnEnteredState;
        }

        private void UpdateState(PlayerState state)
        {
            _state.OnExitState();
            _states.TryGetValue(state, out _state);
            _state.OnEnterState();
            StateUpdated.Invoke(_state); 
        }

        public void CompleteState(MoveState state)
        {
            switch (state.PlayerState)
            {
                case PlayerState.IDLE:
                case PlayerState.PLOW:
                case PlayerState.PICK:
                    UpdateState(PlayerState.WALK);
                    break;
                case PlayerState.WALK:
                    UpdateState(PlayerState.IDLE);
                    break;
            }
        }
        
        private void MoveStateOnCompleteState(MoveState state) // Метод для обновления state
        {
            CompleteState(state);
        }
    }
}