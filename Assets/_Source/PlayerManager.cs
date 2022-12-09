using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NE.InputManager;
using NE.Units.Player;

namespace NE.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        public Transform playerUnits;
        public Transform enemyUnits;
        
        private void  Awake()
        {
            instance = this;
            UnitHandler.instance.SetBasicUnitStats(playerUnits);
            UnitHandler.instance.SetBasicUnitStats(enemyUnits);

        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }

}
