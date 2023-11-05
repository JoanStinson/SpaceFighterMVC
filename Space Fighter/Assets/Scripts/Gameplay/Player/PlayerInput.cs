using System;
using UnityEngine;

namespace JGM.Game
{
    public class PlayerInput : MonoBehaviour
    {
        public IInputService input { get; private set; }
        public event Action onFireWeapon = delegate { };

        private IInputService m_userInputService;
        private IInputService m_botInputService;
        private bool m_botActive;

        private void Awake()
        {
            m_userInputService = new UserInputService();
            m_botInputService = new BotInputService();
            input = m_userInputService;
        }

        private void Update()
        {
            input.ReadInput();

            if (input.shootProjectile)
            {
                onFireWeapon();
            }
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeInputService();
            }
        }

        private void ChangeInputService()
        {
            m_botActive = !m_botActive;
            input = m_botActive ? m_botInputService : m_userInputService;
        }
    }
}