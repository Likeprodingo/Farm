using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enum;
using Pool;
using UnityEngine;
using DG.Tweening;
using GameController;
using GamePlay.Enviroment.TIleStates;
using GamePlay.Enviroment.TIleStates.Impl;

namespace GamePlay
{
    public class Field : MonoBehaviour
    {
        public static event Action UpdateState = delegate { };

        [SerializeField] private int _fieldWidth = 3;
        [SerializeField] private int _fieldHeight = 3;
        [SerializeField] private Tile _productionTile = default;

        [SerializeField] private float _marginVertical = 3f;
        [SerializeField] private float _marginHorizontal = 3f;

        [SerializeField] private float _fieldDelay = 1f;

        private int _tileProcessed = 0;
        private int _totalTileCount;

        private FieldState _state = FieldState.DIRTY;

        private Queue<FieldState> _fieldStates = new Queue<FieldState>();
        
        private void OnEnable()
        {
            GameManager.GameStarted += GameManagerGameStarted;
            TileState.StateCompleted += TileStateOnStateCompleted;
        }
        
        private void OnDisable()
        {
            GameManager.GameStarted -= GameManagerGameStarted;
            TileState.StateCompleted -= TileStateOnStateCompleted;
        }

        private void GameManagerGameStarted()
        {
            SpawnField();
        }
        
        private void TileStateOnStateCompleted(TileState obj)
        {
            _tileProcessed++;
            if (_tileProcessed == _totalTileCount)
            {
                _tileProcessed = 0;
                StartCoroutine(UpdateStateInvoke());
            }
        }

        private IEnumerator UpdateStateInvoke()
        {
            yield return new WaitForSeconds(_fieldDelay);
            UpdateState.Invoke();
        }
        
        private void SpawnField()
        {
            var pool = ObjectPool.Instance;
            Vector3 currentSpawnPosition = transform.position;
            _totalTileCount = _fieldHeight * _fieldWidth;
            for (int i = 0; i < _fieldHeight; i++)
            {
                currentSpawnPosition.z += _marginVertical * i;
                for (int j = 0; j < _fieldWidth; j++)
                {
                    pool.Get<Tile>(_productionTile, currentSpawnPosition, Quaternion.identity, transform);
                    currentSpawnPosition.x += _marginHorizontal;
                }
                currentSpawnPosition = transform.position;
            }
        }
    }
}