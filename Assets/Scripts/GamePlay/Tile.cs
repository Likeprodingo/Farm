using System;
using Assets.Scripts.Enum;
using Pool;
using UnityEngine;

namespace GamePlay
{
    public class Tile : PooledObject
    {
        [SerializeField] private Production _production = default;
        [SerializeField] private Transform _spawnProdPosition = default;
        [SerializeField] private Renderer _renderer = default;
        [SerializeField] private Material _plowed = default;
        public event Action<FieldState> UpdatedState = delegate {  };

        private Material _startMaterial;
        [SerializeField] private FieldState _state;
        private Production _activeProd;

        public override void DeInit()
        {
            base.DeInit();
            Field.UpdateState -= FieldUpdateState;
        }

        public override void Init()
        {
            base.Init();
            _startMaterial = _renderer.material;
            Field.UpdateState += FieldUpdateState;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCollector _collector))//TODO переосмыслить иф
            {
                var pool = ObjectPool.Instance;
                switch (_state)
                {
                    case FieldState.PLOWED:
                        _activeProd = pool.Get<Production>(_production, _spawnProdPosition.position, Quaternion.identity, transform);
                        _activeProd.Picked += ProductionPicked;
                        _state = FieldState.PROCESSED;
                        break;
                    case FieldState.DIRTY:
                        _state = FieldState.PROCESSED;
                        UpdatedState.Invoke(FieldState.DIRTY);
                        _renderer.material = _plowed;
                        break;
                }
                
            }
        }

        private void FieldUpdateState(FieldState obj)
        {
            _state = obj;
        }
        
        private void ProductionPicked()
        {
            _activeProd.Picked -= ProductionPicked;
            UpdatedState.Invoke(_state);
            _renderer.material = _startMaterial;
        }
    }
}