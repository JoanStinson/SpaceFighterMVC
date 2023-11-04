using System.Collections;
using UnityEngine;

namespace JGM.Game
{
    public class SpaceFighter : MonoBehaviour
    {
        [SerializeField]
        private GameView m_gameView;

        private void Start()
        {
            m_gameView.Initialize();
        }
    }
}