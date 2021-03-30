using System;
using GamePlay;
using UnityEngine;

namespace GameController
{
    public partial class AssetManager
    {
        [Serializable]
        public class TilePrefab
        {
            [SerializeField] private Tile _prefab = default;
            [SerializeField] private int _count = default;

            public Tile prefab => _prefab;

            public int count => _count;
        }
        
        [Serializable]
        public class ProductionPrefab
        {
            [SerializeField] private Production _prefab = default;
            [SerializeField] private int _count = default;

            public Production prefab => _prefab;

            public int count => _count;
        }
    }
}