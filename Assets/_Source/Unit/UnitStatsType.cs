using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE.Units
{
    public class UnitStatsType : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float cost, aggroRange, attackRange, attack, health, armor, attackSpeed;
        }
    }
}

