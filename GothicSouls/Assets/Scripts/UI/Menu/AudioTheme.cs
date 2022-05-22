using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AudioTheme : MonoBehaviour
    {
        private static AudioTheme _instance;

        public static AudioTheme Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(_instance);
            }
        }
    }
}
