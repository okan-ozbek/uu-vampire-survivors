using System;
using UnityEngine;

namespace Serializables
{
    [Serializable]
    public abstract class EntityData : Data
    {
        [Header("Entity Status")]
        public float health;
    }
}