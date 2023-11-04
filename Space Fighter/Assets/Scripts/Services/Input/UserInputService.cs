using UnityEngine;

namespace JGM.Game
{
    public class UserInputService : IInputService
    {
        public float vertical { get; private set; }
        public bool shootProjectile { get; private set; }

        public void ReadInput()
        {
            vertical = Input.GetAxis("Vertical");
            shootProjectile = Input.GetButtonDown("Submit");
        }
    }
}