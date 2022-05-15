using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class FogWall : MonoBehaviour
    {
        public bool alwaysActive;

        private void Awake()
        {
            if (!alwaysActive)
            {
                gameObject.SetActive(false);
            }
        }

        public void ActiveFogWall()
        {
            gameObject.SetActive(true);
        }

        public void DesactivateFogWall()
        {
            gameObject.SetActive(false);
        }
    }
}
