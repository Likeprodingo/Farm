using System;
using Util;

namespace GameController
{
    public class GameManager : GameObjectSingleton<GameManager>
    {
        
        public static event Action GameEnded = delegate { };
        public static event Action GameStarted = delegate { };
        
        protected override void Init()
        {
            base.Init();
        }
        
        protected override void DeInit()
        {

        }

        private void PlayerScore_GameLose()
        {
            EndLevel();
        }
        
        private void PlayerScore_GameWin()
        {
            EndLevel();
        }
        
        private void EndLevel()
        {
            GameEnded?.Invoke();
        }

        public void StartLevel()
        {
            GameStarted?.Invoke();
        }
    }
}