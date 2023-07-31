using System.Collections.Generic;

namespace GameArki.FSMNS {

    public class FSM {

        SortedDictionary<int, IFSMState> all;
        IFSMState curState;
        bool isActive;

        public FSM() {
            this.all = new SortedDictionary<int, IFSMState>();
            this.isActive = false;
        }

        public int GetCurrentStateID() {
            if (curState != null) {
                return curState.StateID;
            } else {
                return -1;
            }
        }

        public void Register(IFSMState action) {
            all.Add(action.StateID, action);
        }

        public void Activate() {
            isActive = true;
        }

        public void Deactivate() {
            isActive = false;
        }

        public void Enter(int stateID) {

            if (curState != null && curState.StateID == stateID) {
                return;
            }

            bool has = all.TryGetValue(stateID, out var state);
            if (!has) {
                System.Console.WriteLine($"[err] 不存在状态 {stateID}");
                return;
            }

            if (curState != null) {
                curState.Exit();
            }

            state.Enter();

            curState = state;

        }

        public void Execute() {

            if (!isActive || curState == null) {
                return;
            }

            bool running = curState.Execute();
            if (!running) {
                curState.Exit();
                curState = null;
            }

        }

    }

}