using System.Collections.Generic;

namespace GameArki.FPPhysics2D {

    internal class Collision2DEventCenter {

        Queue<InternalCollision2DEventModel> enterQueue;
        Queue<InternalCollision2DEventModel> stayQueue;
        Queue<InternalCollision2DEventModel> exitQueue;

        internal Collision2DEventCenter() {
            this.enterQueue = new Queue<InternalCollision2DEventModel>();
            this.stayQueue = new Queue<InternalCollision2DEventModel>();
            this.exitQueue = new Queue<InternalCollision2DEventModel>();
        }

        internal void EnqueueEnter(in InternalCollision2DEventModel ev) {
            enterQueue.Enqueue(ev);
        }

        internal bool TryDequeueEnter(out InternalCollision2DEventModel ev) {
            return enterQueue.TryDequeue(out ev);
        }

        internal void EnqueueStay(in InternalCollision2DEventModel ev) {
            stayQueue.Enqueue(ev);
        }

        internal bool TryDequeueStay(out InternalCollision2DEventModel ev) {
            return stayQueue.TryDequeue(out ev);
        }

        internal void EnqueueExit(in InternalCollision2DEventModel ev) {
            exitQueue.Enqueue(ev);
        }

        internal bool TryDequeueExit(out InternalCollision2DEventModel ev) {
            return exitQueue.TryDequeue(out ev);
        }
        
    }

}