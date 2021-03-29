using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;
        public bool IsCapsLock = false;
        public string AdditiveText;
        public string AdditiveTextRight;

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            if(IsCapsLock)
            {
                GetComponent<Text>().text = AdditiveText.ToUpper() + LocalizationManager.Localize(LocalizationKey).ToUpper() + AdditiveTextRight.ToUpper();
            }
            else
            {
                GetComponent<Text>().text = AdditiveText + LocalizationManager.Localize(LocalizationKey) + AdditiveTextRight;
            }
        }
    }
}