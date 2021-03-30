using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enum;
using Pool;
using UnityEngine;
using DG.Tweening;
using GameController;

namespace GamePlay
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private int _fieldWidth = 3;
        [SerializeField] private int _fieldHeight = 3;
        [SerializeField] private Tile _productionTile = default;

        [SerializeField] private float _marginVertical = 3f;
        [SerializeField] private float _marginHorizontal = 3f;

        private List<Tile> _activeTiles = new List<Tile>();
        
        public static event Action<FieldState> UpdateState = delegate {  };

        private int _tileProcessed = 0;
        
        private Vector3 _currentSpawnPosition;

        private void OnEnable()
        {
            GameManager.GameStarted += GameManagerGameStarted;
        }
        
        private void GameManagerGameStarted()
        {
            SpawnField();
        }

        private void OnDisable()
        {
            GameManager.GameStarted -= GameManagerGameStarted;
            foreach (var tile in _activeTiles)
            {
                tile.UpdatedState -= TileUpdateState;
                ObjectPool.Instance.FreeObject(tile);
            }
            _activeTiles.Clear();
        }

        private void TileUpdateState(FieldState state)
        {
            _tileProcessed++;
            if (_tileProcessed == _activeTiles.Count)
            {
                _tileProcessed = 0;
                UpdateState.Invoke(state==FieldState.DIRTY ? FieldState.PLOWED : FieldState.DIRTY);
            }
        }
        
        private void SpawnField()
        {
            var pool = ObjectPool.Instance;
            _currentSpawnPosition = transform.position;
            for (int i = 0; i < _fieldHeight; i++)
            {
                _currentSpawnPosition.z += _marginVertical * i;
                for (int j = 0; j < _fieldWidth; j++)
                {
                    Tile _tile = pool.Get<Tile>(_productionTile, _currentSpawnPosition, Quaternion.identity, transform);
                    _tile.UpdatedState += TileUpdateState;
                    _activeTiles.Add(_tile);
                    _currentSpawnPosition.x += _marginHorizontal;
                }
                _currentSpawnPosition = transform.position;
            }
        }
    }
}