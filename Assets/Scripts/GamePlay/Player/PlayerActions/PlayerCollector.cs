using System;
using Assets.Scripts.Enum;
using UnityEngine;

namespace GamePlay
{
    public class PlayerCollector : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer = default;
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Tile tile))
            {
                tile.Process();
                _renderer.material.color = tile.state.Material.color;
            }
        }
    }
}