using UnityEngine;

namespace JGM.Game
{
    public class EnemyShipView : EnemyView
    {
        protected override void Move()
        {
            transform.position -= Vector3.right * m_moveSpeed * Time.deltaTime;
        }
    }
}