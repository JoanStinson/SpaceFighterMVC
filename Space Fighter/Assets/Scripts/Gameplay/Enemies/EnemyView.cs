using System;
using UnityEngine;

namespace JGM.Game
{
    public abstract class EnemyView : MonoBehaviour
    {
        public abstract void Initialize(Vector3 startPosition, ComponentPool<EnemyView> pool, bool startMovingUp);

        public abstract void Return();
    }
}