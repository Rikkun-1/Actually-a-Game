using System;
using UnityEditor;

namespace Entitas.VisualDebugging.Unity.Editor {

    public class LongTypeDrawer : ITypeDrawer {

        public bool HandlesType(Type type) {
            return type == typeof(long);
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {
            return EditorGUILayout.LongField(memberName, (long)value);
        }
    }
}