using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager enemy);
    }
}
