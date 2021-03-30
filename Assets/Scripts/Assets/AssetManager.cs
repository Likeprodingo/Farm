using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using Util;

namespace GameController
{
    public partial class AssetManager : GameObjectSingleton<AssetManager>
    {
        [SerializeField] private List<ProductionPrefab> _productionPrefabs = new List<ProductionPrefab>();
        [SerializeField] private List<TilePrefab> _tilePrefabs = new List<TilePrefab>();

        public List<ProductionPrefab> productionPrefabs => _productionPrefabs;

        public List<TilePrefab> tilePrefabs => _tilePrefabs;
    }
}