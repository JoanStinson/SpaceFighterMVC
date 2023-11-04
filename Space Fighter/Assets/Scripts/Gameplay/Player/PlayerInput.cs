using System;
using UnityEngine;

namespace JGM.Game
{
    public class PlayerInput : MonoBehaviour
    {
        public IInputService input { get; private set; }
        public event Action onFireWeapon = delegate { };

        private void Awake()
        {
            input = new UserInputService();
        }

        private void Update()
        {
            input.ReadInput();

            if (input.shootProjectile)
            {
                onFireWeapon();
            }
        }
    }
}