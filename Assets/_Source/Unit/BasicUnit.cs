using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NE.Units.Player
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Create New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum UnitType
        {
            Worker,
            Warrior,
            Healer
        };
        [Space(15)]
        [Header("Unit Settings")]
        [Space(15)]
        public UnitType type;
        public bool isPlayerUnit;
        public string unitName;
        public GameObject playerUnitPrefab;
        public GameObject enemyUnitPrefab;
        
        [Space(15)]
        [Header("Unit Stats")]
        [Space(15)]
        public UnitStatsType.Base baseStat;
    }
}


