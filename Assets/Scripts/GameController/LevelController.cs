using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using Util;

namespace GameController
{
    public class LevelController: MonoBehaviour
    {
        [SerializeField] private readonly List<Field> _fields = new List<Field>();
        
        protected void OnEnable()
        {
            GameManager.GameStarted += GameManager_GameStarted;
        }
        protected void OnDisable()
        {
            GameManager.GameStarted -= GameManager_GameStarted;
        }
        
        private void GameManager_GameStarted()
        {
            
        }
        
    }
}