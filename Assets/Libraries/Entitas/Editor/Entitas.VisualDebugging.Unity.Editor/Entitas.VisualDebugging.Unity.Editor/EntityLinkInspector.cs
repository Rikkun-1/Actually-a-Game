using System.Linq;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor {
    
    [CustomEditor(typeof(EntityLink))]
    [CanEditMultipleObjects]
    public class EntityLinkInspector : UnityEditor.Editor {
        
        public override bool RequiresConstantRepaint() => true;

        public override void OnInspectorGUI() {
            if (targets.Length > 1)
            {
                var entities = targets
                                .Select(t => ((EntityLink)t).entity)
                                .ToArray();

                EntityDrawer.DrawMultipleEntities(entities);
            }
            else
            {
                var link = (EntityLink)target;

                if (link.entity != null) {
                    if (GUILayout.Button("Unlink")) {
                        link.Unlink();
                    }
                }

                if (link.entity != null) {
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(link.entity.ToString());

                    if (GUILayout.Button("Show entity")) {
                        Selection.activeGameObject = FindObjectsOfType<EntityBehaviour>()
                            .Single(e => e.entity == link.entity).gameObject;
                    }

                    EditorGUILayout.Space();

                    EntityDrawer.DrawEntity(link.entity);
                } else {
                    EditorGUILayout.LabelField("Not linked to an entity");
                }
            }
        }
    }
}
