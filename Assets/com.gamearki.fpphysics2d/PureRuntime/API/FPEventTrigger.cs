using System;

namespace GameArki.FPPhysics2D.API {

    public class FPEventTrigger : IFPEventTrigger {

        public event Action<TriggerEventArgs> OnTriggerEnterHandle;
        void IFPEventTrigger.TriggerEnter(TriggerEventArgs args) {
            OnTriggerEnterHandle?.Invoke(args);
        }

        public event Action<TriggerEventArgs> OnTriggerStayHandle;
        void IFPEventTrigger.TriggerStay(TriggerEventArgs args) {
            OnTriggerStayHandle?.Invoke(args);
        }

        public event Action<TriggerEventArgs> OnTriggerExitHandle;
        void IFPEventTrigger.TriggerExit(TriggerEventArgs args) {
            OnTriggerExitHandle?.Invoke(args);
        }

        public event Action<CollisionEventArgs> OnCollisionEnterHandle;
        void IFPEventTrigger.CollisionEnter(CollisionEventArgs args) {
            OnCollisionEnterHandle?.Invoke(args);
        }

        public event Action<CollisionEventArgs> OnCollisionStayHandle;
        void IFPEventTrigger.CollisionStay(CollisionEventArgs args) {
            OnCollisionStayHandle?.Invoke(args);
        }

        public event Action<CollisionEventArgs> OnCollisionExitHandle;
        void IFPEventTrigger.CollisionExit(CollisionEventArgs args) {
            OnCollisionExitHandle?.Invoke(args);
        }

    }

}