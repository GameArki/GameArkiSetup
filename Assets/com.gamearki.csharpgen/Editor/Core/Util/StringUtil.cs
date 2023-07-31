namespace GameArki.CSharpGen {
    public static class StringUtil {
        public static string Repeat(string str, int times) {
            string tar = "";
            for (int i = 0; i < times; i += 1) {
                tar += str;
            }
            return tar;
        }
    }
}
