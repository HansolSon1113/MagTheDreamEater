using DG.Tweening;
using InGame.Managers;
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
        private bool isColliding;
        [SerializeField] private bool dream;

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
            
            eatAction = InputSystem.actions.FindAction("Eat");
            eatAction.Enable();
            throwAction = InputSystem.actions.FindAction("Throw");
            throwAction.Enable();

            lastEvent = new InputEvent { actionType = InputEvent.Type.None };

            onEatPerformed = ctx => OnAnyAction(InputEvent.Type.Eat);
            onThrowPerformed = ctx => OnAnyAction(InputEvent.Type.Throw);

            eatAction.performed += onEatPerformed;
            throwAction.performed += onThrowPerformed;
        }

        private void Start()
        {
            isColliding = false;
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
            if (!isColliding) return;

            lastEvent.actionType = type;

            ProcessLastEvent();
            lastEvent.actionType = InputEvent.Type.None;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("HitPoint"))
            {
                isColliding = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("HitPoint"))
            {
                scoreManager.score--;
                scoreManager.health--;
                isColliding = false;
            }
        }

        private void ProcessLastEvent()
        {
            if (!isColliding) return;

            transform.DOKill();
            switch (lastEvent.actionType)
            {
                case InputEvent.Type.Eat:
                    if (dream)
                    {
                        scoreManager.score += 2;
                        scoreManager.health++;
                    }
                    break;
                case InputEvent.Type.Throw:
                    if (!dream)
                    {
                        scoreManager.score += 2;
                        scoreManager.health++;
                    }
                    break;
            }

            Destroy(gameObject);
        }
    }
}