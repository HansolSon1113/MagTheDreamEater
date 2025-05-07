using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IEscapable
    {
        public InputAction escapeAction { get; set; }

        void Awake();
    
        void OnDestroy();
    
        void OnEscapePerformed(InputAction.CallbackContext ctx);

        void Escape();
    }
}