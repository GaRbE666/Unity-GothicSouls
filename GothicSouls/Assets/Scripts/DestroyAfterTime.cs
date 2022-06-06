using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy;

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}
