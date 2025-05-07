using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using TMPro;
using UnityEngine;

namespace UI.Stages
{
    public enum StageMenu
    {
        Start, Back
    }

    public interface IStageMenuContainer
    {
        StageMenu stageMenu { get; set; }
    }
    
    public class StagesUIView : MonoBehaviour, IStageMenuContainer, IShowContainer
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private TMP_Text indexText, nameText, descriptionText, latestScoreText, highestScoreText;
        [SerializeField] private Transform camera, player;
        [SerializeField] private List<GameObject> btnOverlay;
        public static StagesUIView Instance;
        private StageMenu _stageBtn = StageMenu.Back;
        public StageMenu stageMenu
        {
            get => _stageBtn;
            set
            {
                _stageBtn = value;
                
                btnOverlay[0].SetActive(false);
                btnOverlay[1].SetActive(false);
                btnOverlay[(int)_stageBtn].SetActive(true);
            }
        }
        
        public int index
        {
            set => indexText.text = "Stage " + (value + 1);
        }

        public string name
        {
            set => nameText.text = value;
        }

        public string description
        {
            set => descriptionText.text = value;
        }

        public int latestScore
        {
            set => latestScoreText.text = value.ToString();
        }

        public int highestScore
        {
            set => highestScoreText.text = value.ToString();
        }

        private void Awake()
        {
            Instance = this;
        }
        
        public void Show()
        {
            panel.DOAnchorPosX(0, 1f);
            camera.DOMove(new Vector3(player.position.x + 4, player.position.y, -10), 1f);
        }

        public void UnShow()
        {
            panel.DOAnchorPosX(1000, 1f);
            camera.DOMove(new Vector3(player.position.x, player.position.y, -10), 1f);
        }
    }
}