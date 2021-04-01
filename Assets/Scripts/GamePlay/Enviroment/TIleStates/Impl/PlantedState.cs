using System;
using Assets.Scripts.Enum;
using Pool;
using UnityEngine;

namespace GamePlay.Enviroment.TIleStates.Impl
{
    public class PlantedState: TileState
    {
        public PlantedState(Material material, Action action) : base(material, action)
        {
            _playerMoveState = PlayerState.PICK;
        }
            
        public override void Process()
        {
            base.Process();
            CompleteState();
        }
    }
}