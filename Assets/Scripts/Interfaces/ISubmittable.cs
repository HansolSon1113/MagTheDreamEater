using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface ISubmittable
    {
        public InputAction submitAction { get; set; }
    
        void Start();
    
        void OnDestroy();

        void OnSubmitPerformed(InputAction.CallbackContext ctx);
    }
}