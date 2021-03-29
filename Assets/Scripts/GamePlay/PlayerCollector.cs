using Assets.Scripts.Enum;
using UnityEngine;

namespace GamePlay
{
    public class PlayerCollector : MonoBehaviour
    {
        public PlayerState State { get; set; } = PlayerState.EMPTY;
    }
}