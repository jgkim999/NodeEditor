using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class NodeEditor : EditorWindow
{
    private List<BaseNode> windows = new List<BaseNode>();
    private Vector2 mousePos;
    private BaseNode selectednode;
    private bool makeTransitionMode = false;

    [MenuItem("Windows/Node Editor")]
    static void ShowEditor()
    {
        //NodeEditor editor = EditorWindow.GetWindow<>();
    }

    public static void DrawNodeCurve(Rect start, Rect end)
    {

    }
}
