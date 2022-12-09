using System;
using System.Collections;
using System.Collections.Generic;
using NE.Player;
using NE.Units.Player;
using UnityEngine;

namespace NE.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private RaycastHit _hit;
        public readonly List<Transform> _selectedUnits = new();
        private bool _isDragging = false;
        private Vector3 _mousePosition;

        private void Awake()
        {
            instance = this;
        }

        private void OnGUI()
        {
            if (_isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(_mousePosition, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectangleBorder(rect, 1, Color.white);
            }
        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit))
                {
                    LayerMask layerHit = _hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8: //Unit Layer
                            SelectUnit(_hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default:
                            _isDragging = true;
                            UnselectUnit();
                            break;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (IsWithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                _isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnit())
            {
                _mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit))
                {
                    LayerMask layerHit = _hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8: //Unit Layer
                            
                            break;
                        case 9:
                            break;
                        default:
                            foreach (Transform unit in _selectedUnits)
                            {
                                PlayerUnit playerUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                playerUnit.MoveUnit(_hit.point);
                            }
                            break;
                    }
                }
            }
        }

        private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if (!canMultiselect)
            {
                UnselectUnit();
            }
            _selectedUnits.Add(unit);
            unit.Find("Highlight").gameObject.SetActive(true);
        }

        private void UnselectUnit()
        {
            foreach (Transform unit in _selectedUnits)
            {
                if (unit != null)
                {
                    unit.Find("Highlight").gameObject.SetActive(false);
                }
            }

            _selectedUnits.Clear();
        }

        private bool IsWithinSelectionBounds(Transform tf)
        {
            if (!_isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds viewportBounds = MultiSelect.GetViewportBounds(cam, _mousePosition,Input.mousePosition);
            return viewportBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }

        private bool HaveSelectedUnit()
        {
            return _selectedUnits.Count > 0;
        }
    }
}

