#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("x/kQIEFpct4knNOpaBsDXKOSe6fI1N2Y+93KzNHe0dvZzNHX1pj5zYg6vAOIOrsbGLu6ubq6ubqItb6xOKyTaNH/LM6xRkzTNZb4Hk//9ceW+B5P//XHsOaIp7677aWbvKCIrqcpY6b/6FO9VebBPJVTjhrv9O1UzNDXytHMwYmuiKy+u+28u6u1+cjR3tHb2czR19aY+c3M0NfK0czBiY4h9JXAD1U0I2RLzyNKzmrPiPd5N8s52H6j47GXKgpA/PBI2IAmrU2NiomMiIuO4q+1i42IioiBiomMiAmI4FTivIo00As3pWbdy0ff5t0EYY7HeT/tYR8hAYr6Q2BtySbGGerI1N2Y6tfXzJj7+Yimr7WIjoiMitrU3ZjLzNnW3NnK3JjM3crVy5jZwZjZy8vN1d3LmNnb293IzNnW2929uLs6ube4iDq5sro6ubm4XCkRsZeIOXu+sJO+ub29v7q6iDkOojkL4R+9scSv+O6ppsxrDzObg/8bbdcNghVMt7a4KrMJma6WzG2EtWParoipvrvtvLKrsvnIyNTdmPHW25aJpz07PaMhhf+PShEj+DaUbAkoqmDq3dTR2dbb3ZjX1pjM0NHLmNvdyrW+sZI+8D5Ptbm5vb24uzq5ubjkD6MFK/qcqpJ/t6UO9STm23DzOK/Pz5bZyMjU3Zbb19WX2cjI1N3b2czR3tHb2czdmNrBmNnWwZjI2crM1tyY29fW3NHM0dfWy5jX3pjNy92cWlNpD8hnt/1Zn3JJ1cBVXw2vr7CTvrm9vb+6ua6m0MzMyMuCl5fP3I2brfOt4aULLE9OJCZ36AJ54Oi/VMWBOzPrmGuAfAkHIvey00eTRLDmiDq5qb677aWYvDq5sIg6ubyIi47iiNqJs4ixvrvtvL6ruu3riatxocpN5bZtx+cjSp27Au039eW1SYWe35gyi9JPtTp3ZlMbl0Hr0uPcwog6uc6Itr677aW3ublHvLy7urmUmNvdyszR3tHb2czdmMjX1NHbwcrZ28zR292Yy8zZzN3V3dbMy5aIExvJKv/r7XkXl/kLQENbyHVeG/SuiKy+u+28u6u1+cjI1N2Y6tfXzAZMyyNWaty3c8H3jGAahkHAR9NwmPv5iDq5moi1vrGSPvA+T7W5ubk6ubi+sZI+8D5P29y9uYg5SoiSvv3Gp/TT6C75MXzM2rOoO/k/izI5noicvrvtvLOrpfnIyNTdmPvdysyY196YzNDdmMzQ3daY2cjI1NHb2Xjbi89Pgr+U7lNit5m2YgLLofcNvrvtpba8rrysk2jR/yzOsUZM0zW+iLe+u+2lq7m5R7y9iLu5uUeIpby+q7rt64mriKm+u+28squy+cjI3zewDJhPcxSUmNfIDoe5iDQP+3cQZMaajXKdbWG3btNsGpybqU8ZFJI+8D5Ptbm5vb24iNqJs4ixvrvtM6ExZkHz1E2/E5qIulCghkDosWvxYM4ni6zdGc8scZW6u7m4uRs6uS0mwrQc/zPjbK6Pi3N8t/V2rNFptyWFS5PxkKJwRnYNAbZh5qRuc4WY2dbcmNvdyszR3tHb2czR19aYyNTdmPHW25aJnoicvrvtvLOrpfnI6BIybWJcRGixv48Izc2Z");
        private static int[] order = new int[] { 17,4,17,32,31,15,15,11,48,55,11,26,58,15,42,40,42,37,52,23,55,49,38,47,59,36,39,43,53,35,55,51,33,48,47,56,53,44,46,59,58,50,58,47,53,59,50,57,55,56,53,56,54,56,58,57,58,58,59,59,60 };
        private static int key = 184;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
