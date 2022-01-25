using UnityEngine;
using UnityEditor;
 
public class SelectParent : EditorWindow
{
    [MenuItem("Edit/Select parent &c")]
    static void SelectParentOfObject()
    {
        var objects  = Selection.objects;
        
        for (int i = 0; i < objects.Length; i++)
        {
            var gameObject = (GameObject)objects[i];
            objects[i] = gameObject.transform.parent.gameObject;
        }

        Selection.objects = objects;
    }
}