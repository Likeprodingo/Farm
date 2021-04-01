using System;
using Assets.Scripts.Enum;
using Pool;
using UnityEngine;

namespace GamePlay.Enviroment.TIleStates.Impl
{
    public class PlowedState: TileState
    {
        public PlowedState(Material material, Action action) : base(material, action)
        {
            _playerMoveState = PlayerState.WALK; //TODO посев должен быть
        }
    }
}