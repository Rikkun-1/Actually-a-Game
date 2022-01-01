using System;
using UnityEditor;

namespace Entitas.VisualDebugging.Unity.Editor {

    public class EnumTypeDrawer : ITypeDrawer {

        public bool HandlesType(Type type) {
            return type.IsEnum;
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {
            if (memberType.IsDefined(typeof(FlagsAttribute), false)) {
#pragma warning disable CS0618 // Type or member is obsolete
                return EditorGUILayout.EnumFlagsField(memberName, (Enum)value);
#pragma warning restore CS0618 // Type or member is obsolete
            }
            return EditorGUILayout.EnumPopup(memberName, (Enum)value);
        }
    }
}
