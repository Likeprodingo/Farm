using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "AnimationKeys", menuName = "AnimationKeySet", order = 0)]
    public class AnimationData : ScriptableObject
    {
        public string _paramName = "animation";
        public int _idle = 1;
        public int _walk = 15;
        public int _dig = 24;
        public int _pick = 12;
    }
}