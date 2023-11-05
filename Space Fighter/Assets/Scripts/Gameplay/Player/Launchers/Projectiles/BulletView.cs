using UnityEngine;

namespace JGM.Game
{
    public class BulletView : ProjectileView
    {
        protected override void Move()
        {
            transform.position += Vector3.right * m_moveSpeed * Time.deltaTime;
        }
    }
}