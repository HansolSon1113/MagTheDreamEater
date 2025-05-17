using System.Collections.Generic;
using DG.Tweening;
using InGame.Managers;
using SaveData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Notes
{
    public class NoteCollision : MonoBehaviour
    {
        private ScoreManager scoreManager;
        private InputAction eatAction, throwAction;
        private System.Action<InputAction.CallbackContext> onEatPerformed;
        private System.Action<InputAction.CallbackContext> onThrowPerformed;
        private Queue<GameObject> collidedObjects = new Queue<GameObject>();
        private bool isDream;

        private struct InputEvent
        {
            public enum Type
            {
                None,
                Eat,
                Throw
            }

            public Type actionType;
        }

        private InputEvent lastEvent;
        
        private void Awake()
        {
            scoreManager = ScoreManager.Instance;

            var handSetting = GameDataContainer.settingData.settingDataElements.handMode == HandMode.OneHand;
            var eat = handSetting ? "Eat" : "Eat_TwoHand";
            eatAction = InputSystem.actions.FindAction(eat);
            eatAction.Enable();
            var thr = handSetting ? "Throw" : "Throw_TwoHand";
            throwAction = InputSystem.actions.FindAction(thr);
            throwAction.Enable();

            lastEvent = new InputEvent { actionType = InputEvent.Type.None };

            onEatPerformed = ctx => OnAnyAction(InputEvent.Type.Eat);
            onThrowPerformed = ctx => OnAnyAction(InputEvent.Type.Throw);

            eatAction.performed += onEatPerformed;
            throwAction.performed += onThrowPerformed;
        }

        private void OnDestroy()
        {
            eatAction.performed -= onEatPerformed;
            eatAction.Disable();
            throwAction.performed -= onThrowPerformed;
            throwAction.Disable();
        }

        private void OnAnyAction(InputEvent.Type type)
        {
            var animator = InGameMagAnimationManager.Instance;
            switch (type)
            {
                case InputEvent.Type.Eat:
                    animator.eat = true;
                    break;
                case InputEvent.Type.Throw:
                    animator.thr = true;
                    break;
            }
            
            if (collidedObjects.Count <= 0) return;
            lastEvent.actionType = type;
            ProcessLastEvent();
            lastEvent.actionType = InputEvent.Type.None;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("DreamNote"))
            {
                isDream = true;
            }
            else if(other.gameObject.CompareTag("NightmareNote"))
            {
                isDream = false;
            }
            else
            {
                return;
            }
            
            collidedObjects.Enqueue(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(collidedObjects.Count > 0)
            {
                collidedObjects.Dequeue();
            }
        }

        private void ProcessLastEvent()
        {
            switch (lastEvent.actionType)
            {
                case InputEvent.Type.Eat:
                    if (isDream)
                    {
                        scoreManager.score++;
                    }
                    else
                    {
                        scoreManager.health -= 4;
                    }
                    break;
                case InputEvent.Type.Throw:
                    if (!isDream)
                    {
                        scoreManager.score++;
                    }
                    else
                    {
                        scoreManager.health -= 4;
                    }
                    break;
            }
            
            var obj = collidedObjects.Dequeue();
            obj.transform.DOKill();
            Destroy(obj);
        }
    }
}