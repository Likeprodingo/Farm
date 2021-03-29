using System;
using MoreMountains.NiceVibrations;

namespace Assets.Scripts.Managers
{
    public static class VibrationManager
    {
        public static bool IsVibroOn { get; private set; } = true;

        public static void EnableVibro(bool isEnable)
        {
            IsVibroOn = isEnable;
        }

        public static void StartVibration(HapticTypes vibrationType)
        {
            if (IsVibroOn)
            {
                try
                {
                    MMVibrationManager.Haptic(vibrationType, false, true);
                }
                catch (Exception e)
                {
                    //TODO: :ODOT\\
                }
            }
        }
    }
}