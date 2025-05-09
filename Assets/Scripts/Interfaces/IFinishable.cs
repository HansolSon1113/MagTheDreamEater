using System.Collections.Generic;
using DG.Tweening;
using UI.Stages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IFinishable
    {
        IEscapePanel view { get; set; }
        static SceneFinish Instance { get; }

        abstract void Start();
    }

    public abstract class SceneFinish : MonoBehaviour, IFinishable, IPointerClickHandler
    {
        public IEscapePanel view { get; set; }
        public static SceneFinish Instance;

        public void Awake()
        {
            Instance = this;
        }

        public abstract void Start();

        public abstract void Submit();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Submit();
        }
    }
}