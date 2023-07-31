
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace GameArki.CSharpGen {

    public enum VisitLevel {
        Public,
        Private,
        Protected,
        Internal,
    }

    public struct MethodMember {
        public string returnType;
        public string name;
        public List<OneParameterInfo> parameterList;
    } 

    public static class VisitLevelExtention {
        public static string ToFullString(this VisitLevel level) {
            return level.ToString().ToLower();
        }
    }

}