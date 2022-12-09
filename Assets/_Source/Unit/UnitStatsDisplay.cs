using System;
using System.Collections;
using System.Collections.Generic;
using NE.Units.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace NE.Units
{
    public class UnitStatsDisplay : MonoBehaviour
    {
        public float maxHealth, armor, currentHealth;
        [SerializeField] private Image healthBarAmount;
        private bool isPlayerUnit = false;

        private void Start()
        {
            try
            {
                maxHealth = gameObject. GetComponentInParent<Player.PlayerUnit>().baseStats.health;
                armor = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.armor;
                isPlayerUnit = true;
            }
            catch (Exception)
            {
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.health;
                    armor = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.armor;
                    isPlayerUnit = false;
                }
                catch (Exception)
                {
                    Debug.Log("No script found at all");
                }
            }

            currentHealth = maxHealth;
        }

        private void Update()
        {
            HandleHealth();
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - armor;
            currentHealth -= totalDamage;
        }

        private void HandleHealth()
        {
            Camera cam = Camera.main;
            var rotation = cam.transform.rotation;
            gameObject.transform.LookAt(gameObject.transform.position + rotation * Vector3.forward, rotation *Vector3.up);
            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isPlayerUnit)
            {
                InputManager.InputHandler.instance._selectedUnits.Remove(gameObject.transform);
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
}
}

