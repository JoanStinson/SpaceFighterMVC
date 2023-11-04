﻿using System;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class BotInputService : IInputService
    {
        public float vertical { get; private set; }
        public bool shootProjectile { get; private set; }

        public void ReadInput()
        {
            vertical = Random.Range(-1f, 1f);
            shootProjectile = Convert.ToBoolean(Random.Range(0, 1));
        }
    }
}