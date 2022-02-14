/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;

namespace CodeMonkey.Utils {

    /*
     * Various assorted utilities functions
     * */
    public static class UtilsClass
    {
	    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
	    {
		    color ??= Color.white;
		    return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
	    }

	    // Create Text in the World
	    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
	    {
		    GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
		    Transform  transform  = gameObject.transform;
		    transform.SetParent(parent, false);
		    transform.localPosition = localPosition;
		    TextMesh textMesh = gameObject.GetComponent<TextMesh>();
		    textMesh.anchor                                    = textAnchor;
		    textMesh.alignment                                 = textAlignment;
		    textMesh.text                                      = text;
		    textMesh.fontSize                                  = fontSize;
		    textMesh.color                                     = color;
		    textMesh.characterSize                             = 0.15f;
		    textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		    return textMesh;
	    }
    }
}