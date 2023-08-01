using System.Diagnostics;

namespace GameArki.Setup.Editors {

    public static class SetupProcess {

        public static void StartProcess(string exe, string args) {
            var process = new Process();
            process.StartInfo.FileName = exe;
            process.StartInfo.Arguments = "/c" + args;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

    }

}