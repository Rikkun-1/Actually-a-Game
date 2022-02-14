using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor {

    public class TargetTypeDrawer : ITypeDrawer {

        public bool HandlesType(Type type) {
            return type == typeof(Target);
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            var gameTarget = (Target)value;

            Target newValue;
            var newTargetType = (TargetType)EditorGUILayout.EnumPopup(gameTarget.targetType);
            if (newTargetType != gameTarget.targetType)
            {
                newValue = newTargetType switch
                {
                    TargetType.Direction => Target.Direction(Vector2.up),
                    TargetType.Position  => Target.Position(Vector2Int.zero),
                    TargetType.Entity    => Target.Entity(0),
                    _                    => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                newValue = gameTarget.targetType switch
                {
                    TargetType.Direction => Target.Direction(EditorGUILayout.Vector2Field("Direction", gameTarget.direction)),
                    TargetType.Position  => Target.Position(EditorGUILayout.Vector2IntField("Position", gameTarget.position)),
                    TargetType.Entity    => Target.Entity(EditorGUILayout.LongField("Target ID", gameTarget.entityID)),
                    _                    => throw new ArgumentOutOfRangeException()
                };
            }
            
            return newValue;
        }
    }
}