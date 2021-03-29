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
        
        private enum ProductionState
        {
            GROW,
            READY
        }
        
        

        private ProductionState _state;

        public override void SpawnFromPool()
        {
            base.SpawnFromPool();
            StartCoroutine(Grow());
        }

        private IEnumerator Grow()
        {
            var waiter = new WaitForSeconds(_timeDelta);
            _state = ProductionState.GROW;
            while (_growTime > 0)
            {
                yield return waiter;
                
            }
            _state = ProductionState.READY;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCollector _collector))
            {
                Picked.Invoke();
                _collector.State = PlayerState.COLLECT;
            }
        }
        
        
    }
}