namespace GameArki.Setup {

    [System.Serializable]
    public class SetupPackageModel {

        public string title; // ex: "com.gamearki.bufferio"
        public string name; // ex: "com.gamearki.bufferio"
        public string desc; // ex: "二进制序列化"
        public (string version, string suffix)[] versions; // ex: ["1.0.0", "1.1.0"]
        public string gitUrl; // ex: "ssh://git@github.com/gamearki/bufferio.git"
        public string docuUrl; // ex: "https://www.github.com/gamearki/bufferio"
        public string path; // ex: "?path=Assets/com.gamearki.bufferio"
        public string[] dependencies; // ex: ["com.gamearki.bufferio", "com.gamearki.bufferio"]
        public string[] assemblies; // ex: ["GameArki.BufferIO"]

        public string BakeValue(string version) {
            string value = gitUrl + path + "#" + version;
            return value;
        }

    }

}