using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class SoulCountBar : MonoBehaviour
    {
        public Text soulCountText;

        public void SetSoulSountText(int soulCount)
        {
            soulCountText.text = soulCount.ToString();
        }
    }
}
