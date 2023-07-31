namespace GameArki.BTTreeNS {

    internal class BTTreeNodeActionComponent {

        enum BTTreeActionResult : byte {
            Ready,
            Running,
            End,
        }

        IBTTreeAction action;

        BTTreeActionResult actionResult;

        internal BTTreeNodeActionComponent(IBTTreeAction action) {
            this.action = action;
        }

        internal bool Evaluate() {
            if (actionResult == BTTreeActionResult.Running || actionResult == BTTreeActionResult.Ready) {
                return true;
            } else {
                return false;
            }
        }

        internal void TickAction() {
            if (actionResult == BTTreeActionResult.Running) {
                bool keepRunning = action.Execute();
                if (!keepRunning) {
                    actionResult = BTTreeActionResult.End;
                    action.Exit();
                }
            } else if (actionResult == BTTreeActionResult.Ready) {
                action.Enter();
                actionResult = BTTreeActionResult.Running;
            }
        }

        public string GetString() {
            return action.GetType().Name.ToString();
        }

        internal void Reset() {
            actionResult = BTTreeActionResult.Ready;
        }

    }

}