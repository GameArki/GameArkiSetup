namespace GameArki.FPPhysics2D.API {

    public interface IFPEventTrigger {

        void TriggerEnter(TriggerEventArgs args);
        void TriggerStay(TriggerEventArgs args);
        void TriggerExit(TriggerEventArgs args);

        void CollisionEnter(CollisionEventArgs args);
        void CollisionStay(CollisionEventArgs args);
        void CollisionExit(CollisionEventArgs args);

    }

}