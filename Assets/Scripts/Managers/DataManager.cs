using Assets.Scripts.Data;
using BayatGames.SaveGameFree;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        private PlayerData _playerData = new PlayerData();
        private readonly string IDENTIFIER = "playerData";

        #region Unity

        private void Start()
        {
            LoadData();
        }

        #endregion

        #region Public


        #endregion

        #region Private
        private void SaveData()
        {
            SaveGame.Save(IDENTIFIER, _playerData, true);
        }

        private void LoadData()
        {
            if (SaveGame.Exists(IDENTIFIER))
            {
                _playerData = SaveGame.Load(IDENTIFIER, new PlayerData(), true);
            }
        }

        #endregion
    }
}