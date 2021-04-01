using System;
using System.Collections;
using Assets.Scripts.Enum;
using Pool;
using UnityEngine;
using DG.Tweening;

namespace GamePlay
{
    public class Production : PooledObject
    {
        public event Action Picked = () => { };

        [SerializeField] private float _growTime = 3f;
        [SerializeField] private float _timeDelta = 1f;
        [SerializeField] private Vector3 _maxScale = new Vector3(4,4,4);
        [SerializeField] private float _maxGrowPosDelta = 1f;
        [SerializeField] private float _collectActionTime = 0.3f;

        private Vector3 _startScale;

        private Transform currentTransform;
        
        private ProductionState _state;
        
        private enum ProductionState
        {
            EMPTY,
            GROW,
            READY
        }
        
        public override void SpawnFromPool()
        {
            base.SpawnFromPool();
            _state = ProductionState.EMPTY;
            currentTransform = transform;
            _startScale = currentTransform.localScale;
            StartCoroutine(Grow());
        }

        private IEnumerator Grow()
        {
            var waiter = new WaitForSeconds(_timeDelta);
            _state = ProductionState.GROW;
            var _currentTime = _growTime;
            var pos = currentTransform.position;
            while (_currentTime > 0)
            {
                currentTransform.localScale = Vector3.Lerp(_startScale, _maxScale, (_growTime -_currentTime) / _growTime);
                pos.y = _maxGrowPosDelta * _timeDelta / _growTime;
                currentTransform.position = pos;
                yield return waiter;
                _currentTime -= _timeDelta;
            }
            _state = ProductionState.READY;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_state == ProductionState.READY && other.TryGetComponent(out PlayerCollector _collector))
            {
                Picked.Invoke();
                StartCoroutine(CollectAction());
            }
        }

        private IEnumerator CollectAction()
        {
            yield return new WaitForSeconds(_collectActionTime);
            currentTransform.localScale = _startScale;
            ObjectPool.Instance.FreeObject(this);
        }
    }
}