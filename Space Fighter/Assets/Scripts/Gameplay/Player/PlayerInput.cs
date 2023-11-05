using System;
using UnityEngine;

namespace JGM.Game
{
    public class PlayerInput : MonoBehaviour
    {
        public IInputService input { get; private set; }
        public bool bulletsSelected => m_bulletsActive;
        public event Action onFireWeapon = delegate { };

        private IInputService m_userInputService;
        private IInputService m_botInputService;
        private bool m_botActive;
        private bool m_bulletsActive = true;

        private void Awake()
        {
            m_userInputService = new UserInputService();
            m_botInputService = new BotInputService();
            input = m_userInputService;
            m_bulletsActive = true;
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

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon();
            }
        }

        private void ChangeInputService()
        {
            m_botActive = !m_botActive;
            input = m_botActive ? m_botInputService : m_userInputService;
        }

        private void ChangeWeapon()
        {
            m_bulletsActive = !m_bulletsActive;
        }
    }
}