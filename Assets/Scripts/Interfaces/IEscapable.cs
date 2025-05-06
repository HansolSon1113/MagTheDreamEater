using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IEscapable
    {
        public InputAction escapeAction { get; set; }

        void Start();
    
        void OnDestroy();
    
        void OnEscapePerformed(InputAction.CallbackContext ctx);
    }
}