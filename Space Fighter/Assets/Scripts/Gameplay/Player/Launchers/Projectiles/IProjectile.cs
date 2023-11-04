using UnityEngine;

namespace JGM.Game
{
    public interface IProjectile
    {
        void Launch(Transform mountPoint, ComponentPool<BulletView> pool);
    }
}