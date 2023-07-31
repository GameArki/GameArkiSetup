using System.Collections.Generic;

namespace GameArki.FPPhysics2D {

    internal class Trigger2DEventCenter {

        Queue<InternalTrigger2DEventModel> enterQueue;
        Queue<InternalTrigger2DEventModel> stayQueue;
        Queue<InternalTrigger2DEventModel> exitQueue;

        internal Trigger2DEventCenter() {
            this.enterQueue = new Queue<InternalTrigger2DEventModel>();
            this.stayQueue = new Queue<InternalTrigger2DEventModel>();
            this.exitQueue = new Queue<InternalTrigger2DEventModel>();
        }

        // ==== Enter ====
        internal void EnqueueEnter(InternalTrigger2DEventModel ev) {
            enterQueue.Enqueue(ev);
        }

        internal bool TryDequeueEnter(out InternalTrigger2DEventModel ev) {
            return enterQueue.TryDequeue(out ev);
        }

        // ==== Stay ====
        internal void EnqueueStay(InternalTrigger2DEventModel ev) {
            stayQueue.Enqueue(ev);
        }

        internal bool TryDequeueStay(out InternalTrigger2DEventModel ev) {
            return stayQueue.TryDequeue(out ev);
        }

        // ==== Exit ====
        internal void EnqueueExit(InternalTrigger2DEventModel ev) {
            exitQueue.Enqueue(ev);
        }

        internal bool TryDequeueExit(out InternalTrigger2DEventModel ev) {
            return exitQueue.TryDequeue(out ev);
        }

    }

}