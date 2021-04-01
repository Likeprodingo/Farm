using System;
using Assets.Scripts.Enum;
using UnityEngine;

namespace GamePlay.Enviroment.TIleStates.Impl
{
    public class DirtyState : TileState
    {
        public DirtyState(Material material, Action action) : base(material, action)
        {
            _playerMoveState = PlayerState.PLOW;
        }

        public override void Process()
        {
            base.Process();
            CompleteState();
        }
    }
}