using System;
using System.Collections.Generic;
using DG.Tweening;
using UI.Stages;
using UnityEngine;

namespace Interfaces
{
    public interface IEscapePanel
    {
        public int cnt { get; set; }
        public bool escapePanelOn { get; set; }
        public EscapeMenu escapeMenu { get; set; }
    }

    public class EscapePanel : MonoBehaviour, IEscapePanel
    {
        //[SerializeField] private RectTransform overlay;
        private bool _escapePanelOn = false;
        private EscapeMenu _escapeMenu;

        public EscapeMenu escapeMenu
        {
            get => _escapeMenu;
            set
            {
                escapeObjects[(int)_escapeMenu].SetActive(false);

                _escapeMenu = value;

                escapeObjects[(int)value].SetActive(true);
                //overlay.DOMove(escapeTransforms[(int)value].position, duration).SetUpdate(true);
            }
        }

        [SerializeField] private GameObject escapePanel;

        public int cnt { get; set; }

        //[SerializeField] private List<RectTransform> escapeTransforms = new List<RectTransform>();
        [SerializeField] private List<GameObject> escapeObjects = new List<GameObject>();
        [SerializeField] private float duration = 0.1f;

        private void Start()
        {
            cnt = escapeObjects.Count;
        }

        public bool escapePanelOn
        {
            get => _escapePanelOn;
            set
            {
                Time.timeScale = value ? 0 : 1;
                _escapePanelOn = value;
                escapePanel.SetActive(value);
            }
        }
    }
}