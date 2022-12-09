using System;
using System.Collections;
using System.Collections.Generic;
using NE.Player;
using UnityEngine;

namespace NE.Units.Player
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        private BasicUnit worker, warrior, healer;

        public LayerMask playerUnitLayer, enemyUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            enemyUnitLayer = LayerMask.NameToLayer("Enemy");
            playerUnitLayer = LayerMask.NameToLayer("Units");
        }

        public UnitStatsType.Base GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch (type)  
            {
                case "worker":
                    unit = worker;
                    break;
                case "healer":
                    unit = healer;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                default:
                    Debug.Log($"No unit match the pattern: {type}");
                    return null;
            }

            return unit.baseStat;
        }

        public void SetBasicUnitStats(Transform type)
        {
            Transform pUnits = PlayerManager.instance.playerUnits;
            Transform eUnits = PlayerManager.instance.enemyUnits;
            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();

                    if (type == pUnits)
                    {
                        Player.PlayerUnit playerUnit = unit.GetComponent<PlayerUnit>();
                        playerUnit.baseStats = GetBasicUnitStats(unitName);
                    }
                    else if (type == eUnits)
                    {
                        Enemy.EnemyUnit enemyUnit = unit.GetComponent<Enemy.EnemyUnit>();
                        enemyUnit.baseStats = GetBasicUnitStats(unitName);
                    }
                }
            }
        }
    }
}
