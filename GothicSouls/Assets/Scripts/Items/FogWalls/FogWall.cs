using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class FogWall : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
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
