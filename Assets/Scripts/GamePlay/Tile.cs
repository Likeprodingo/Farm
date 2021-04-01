using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enum;
using GamePlay.Enviroment.TIleStates;
using GamePlay.Enviroment.TIleStates.Impl;
using Pool;
using UnityEngine;

namespace GamePlay
{
    public class Tile : PooledObject
    {
        [SerializeField] private Production _production = default;
        [SerializeField] private Transform _spawnProdPosition = default;
        [SerializeField] private Renderer _renderer = default;

        [SerializeField] private Material _plowedMaterial = default;
        [SerializeField] private Material _plantedMaterial = default;
        [SerializeField] private Material _dirtyMaterial = default;
        [SerializeField] private float _updateTime = 0.2f;

        private Production _activeProdaction;
        
        private TileState _state;

        private bool _isUpdated = false; 
        
        private Queue<TileState> _stateQue = new Queue<TileState>();

        public TileState state => _state;

        public override void Init()
        {
            base.Init();
            Field.UpdateState += FieldUpdateState;
            CreateStateQue();
        }

        public override void DeInit()
        {
            base.DeInit();
            Field.UpdateState -= FieldUpdateState;
        }
        
        private void CreateStateQue()
        {
            _stateQue.Enqueue(new DirtyState(_dirtyMaterial, null));
            _stateQue.Enqueue(new PlowedState(_plowedMaterial, null));
            _stateQue.Enqueue(new PlantedState(_plantedMaterial, SpawnProduction));
            _state = _stateQue.Dequeue();
        }

        private void SpawnProduction()
        {
            _activeProdaction = ObjectPool.Instance.Get<Production>(_production, _spawnProdPosition.position, Quaternion.identity, transform);
            _activeProdaction.Picked += ProductionPicked;
        }
        
        private void ProductionPicked()
        {
            _activeProdaction.Picked -= ProductionPicked;
            _state.Process();
            UpdateState();
        }

        public void Process()
        {
            if (!_isUpdated)
            {
                _isUpdated = true;
                _state.Process();
                UpdateState();
            }
        }

        private IEnumerator UpdateView(TileState state)
        {
            yield return new WaitForSeconds(_updateTime);
            _renderer.material = state.Material;
            state.StateAction?.Invoke();
        }
        
        private void UpdateState()
        {
            _stateQue.Enqueue(_state);
            _state = _stateQue.Dequeue();
            StartCoroutine(UpdateView(_state));
        }
        
        private void FieldUpdateState()
        {
            _isUpdated = false;
        }
    }
}