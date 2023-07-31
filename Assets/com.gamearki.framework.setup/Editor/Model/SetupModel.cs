using System;
using System.Collections.Generic;

namespace GameArki.Setup.Editors {

    [Serializable]
    public class SetupModel {

        // Dir
        public string rootDir;
        public List<string> dirs; // Like: Runtime / PureRuntime / Editor

        // Package.json
        public string packageName;
        public string version;
        public string desc;

    }

}