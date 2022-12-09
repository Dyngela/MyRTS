using System;
using System.Collections;
using System.Collections.Generic;
using NE.Units.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace NE.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        public UnitStatsType.Base baseStats;
        private NavMeshAgent _agent;
        private Collider[] rangeColliders;
        private Transform aggroTarget;
        private UnitStatsDisplay aggroUnit;
        private bool hasAggro = false;
        private float distance;
        private float attackCooldown;

        private void Start()
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            attackCooldown -= Time.deltaTime;
            if(!hasAggro)
                CheckForEnemyTarget();
            else
            {
                MoveToAggroTarget();
                Attack();
            }
        }

        private void CheckForEnemyTarget()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);
            foreach (Collider unit in rangeColliders)
            {
                if (unit.gameObject.layer == UnitHandler.instance.playerUnitLayer)
                {
                    aggroTarget = unit.gameObject.transform;
                    aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatsDisplay>();
                    hasAggro = true;
                    break;
                }
            }
        }

        private void Attack()
        {
            if (attackCooldown <= 0 && distance < baseStats.attackRange + 1)
            {
                aggroUnit.TakeDamage(baseStats.attack);
                attackCooldown = baseStats.attackSpeed;
            }
        }

        private void MoveToAggroTarget()
        {
            if (aggroTarget == null)
            {
                _agent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {
                distance = Vector3.Distance(aggroTarget.position, transform.position);
                _agent.stoppingDistance = (baseStats.attackRange + 1);
                if (distance <= baseStats.aggroRange)
                {
                    _agent.SetDestination(aggroTarget.position);
                }
            }
        }
    }
}

