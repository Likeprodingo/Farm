#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("8N8F65v2ubVk+wOzSndQ5ekTtn9QoYlQf+JjSbZAmhqEmWJS30aRKaMRkrGjnpWauRXbFWSekpKSlpOQS5hP3l92aEzqYnEAh/Oa0ZHK3N7tTtGK3lWwQ1vXfMywr/WPxc+HkH+tUYwa/D6WxsdREggZOQplH/QlY2MYVW2CFSffVAL0NQTCYC9qLGiCs2/MGwk0R1OilCXTaiOrBDs97K6UgEBAb3JY8oZkJPW7rmaBFzrDeKFAshymXayWgOiZ3FETf/xlP5gRkpyToxGSmZERkpKTLSncsDkwR0W1cD2M+KydcpUfohJkDrYf3+svkfPjzJYNkpsWODnufciJu33YtbDV4aBDl21Ch3yohBNCBWYXBm6wZ/rcDcNFXsNNUJGQkpOS");
        private static int[] order = new int[] { 6,8,6,10,10,6,7,12,12,13,12,12,13,13,14 };
        private static int key = 147;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
