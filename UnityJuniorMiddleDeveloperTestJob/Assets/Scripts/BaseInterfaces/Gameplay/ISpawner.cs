﻿using UnityEngine;

namespace BaseInterfaces.Gameplay
{
    public interface ISpawner
    {
        void Construct(float interval, Vector3 moveTargetPosition, float speed, int maxHP);
        void WarmUp();
        void Spawn();
    }
}