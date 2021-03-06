﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class NodeEditor : EditorWindow
{
    private List<BaseNode> windows = new List<BaseNode>();
    private Vector2 mousePos;
    private BaseNode selectednode;
    private bool makeTransitionMode = false;
    Vector2 scrollPos;
    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    [MenuItem("Windows/Node Editor")]
    static void ShowEditor()
    {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
        editor.stopwatch.Start();
    }

    void Update()
    {
        long dTime = stopwatch.ElapsedMilliseconds;
        float deltaTime = ((float) dTime) / 1000;
        foreach (BaseNode b in windows)
        {
            b.Tick(deltaTime);
        }
        stopwatch.Reset();
        stopwatch.Start();

        Repaint();
    }

    void OnGUI()
    {
        // vertical space
        //GUILayout.Space(20);
        //GUILayout.BeginVertical();
        //GUILayout.BeginHorizontal();

        Event e = Event.current;

        mousePos = e.mousePosition;

        if (e.button == 1 && !makeTransitionMode)
        {
            if (e.type == EventType.MouseDown)
            {
                bool clickedOnWindows = false;
                int selectIndex = -1;

                for (int i = 0; i < windows.Count; ++i)
                {
                    if (windows[i].windowRect.Contains(mousePos))
                    {
                        selectIndex = i;
                        clickedOnWindows = true;
                        break;
                    }
                }

                if (!clickedOnWindows)
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Add Input Node"), false, ContextCallback, "inputNode");
                    menu.AddItem(new GUIContent("Add Output Node"), false, ContextCallback, "outputNode");
                    menu.AddItem(new GUIContent("Add Calculation Node"), false, ContextCallback, "calcNode");
                    menu.AddItem(new GUIContent("Add Comparison Node"), false, ContextCallback, "compNode");
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Add GameObject Node"), false, ContextCallback, "goActive");

                    menu.ShowAsContext();
                    e.Use();
                }
                else
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, "makeTransition");
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");

                    menu.ShowAsContext();
                    e.Use();
                }
            }
        }
        else if (e.button == 0 && e.type == EventType.mouseDown && makeTransitionMode)
        {
            bool clickedOnWindows = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; ++i)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindows = true;
                    break;
                }
            }

            if (clickedOnWindows && !windows[selectIndex].Equals(selectednode))
            {
                windows[selectIndex].SetInput((BaseInputNode) selectednode, mousePos);
                makeTransitionMode = false;
                selectednode = null;
            }

            if (!clickedOnWindows)
            {
                makeTransitionMode = false;
                selectednode = null;
            }

            e.Use();
        }
        else if (e.button == 0 && e.type == EventType.mouseDown && !makeTransitionMode)
        {
            bool clickedOnWindows = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; ++i)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindows = true;
                    break;
                }
            }

            if (clickedOnWindows)
            {
                BaseInputNode nodeToChange = windows[selectIndex].ClickedOnInput(mousePos);
                if (nodeToChange != null)
                {
                    selectednode = nodeToChange;
                    makeTransitionMode = true;
                }
            }
        }

        if (makeTransitionMode && selectednode != null)
        {
            Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
            DrawNodeCurve(selectednode.windowRect, mouseRect);
            Repaint();
        }

        float width = 0;
        float height = 0;

        CalcWindowsWidthHeight(width, height);

//         scrollPos = GUILayout.BeginScrollView(scrollPos, true, true);
//         {
            DrawCurves();
            DrawWindows();
      //  }
        //GUILayout.EndScrollView();
    }

    void CalcWindowsWidthHeight(float width, float height)
    {
        float minX = 0.0f;
        float maxX = 0.0f;
        float minY = 0.0f;
        float maxY = 0.0f;
        foreach (BaseNode n in windows)
        {
            float topX = n.windowRect.position.x;
            float bottomX = n.windowRect.position.x + n.windowRect.width;
            float topY = n.windowRect.position.y;
            float bottomY = n.windowRect.position.y + n.windowRect.height;
            if (topX < minX)
                minX = topX;
            if (bottomX > maxX)
                maxX = bottomX;
            if (topY < minY)
                minY = topY;
            if (bottomY > maxY)
                maxY = bottomY;
        }
        width = maxX - minX;
        height = maxY - minY;

        if (position.width > width)
            width = position.width;
        if (position.height > height)
            height = position.height;

        if (width < 1024)
            width = 1024;
        if (height < 1024)
            height = 1024;

        GUILayout.Label("width " + width);
        GUILayout.Label("height " + height);
        /*
        //GUILayout.Label("minX " + minX);
        //GUILayout.Label("maxX " + maxX);
        //GUILayout.Label("minY " + minY);
        //GUILayout.Label("maxY " + maxY);
        */

    }

    void DrawCurves()
    {
        foreach (BaseNode n in windows)
        {
            n.DrawCurves();
        }
    }

    void DrawWindows()
    {
        BeginWindows();
        for (int i = 0; i < windows.Count; ++i)
        {
            windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
        }
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        windows[id].DrawWindow();
        GUI.DragWindow();
    }

    void ContextCallback(object obj)
    {
        string clb = obj.ToString();
        if (clb.Equals("inputNode"))
        {
            InputNode inputNode = new InputNode();
            inputNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 150);
            windows.Add(inputNode);
        }
        else if (clb.Equals("outputNode"))
        {
            OutputNode outputNode = new OutputNode();
            outputNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 100);
            windows.Add(outputNode);
        }
        else if (clb.Equals("calcNode"))
        {
            CalcNode calcNode = new CalcNode();
            calcNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 100);
            windows.Add(calcNode);
        }
        else if (clb.Equals("compNode"))
        {
            ComparisonNode compNode = new ComparisonNode();
            compNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 100);
            windows.Add(compNode);
        }
        else if (clb.Equals("goActive"))
        {
            GameObjectActive goNode = new GameObjectActive();
            goNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 100);
            windows.Add(goNode);
        }
        else if (clb.Equals("makeTransition"))
        {
            bool clickedOnWindows = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; ++i)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindows = true;
                    break;
                }
            }

            if (clickedOnWindows)
            {
                selectednode = windows[selectIndex];
                makeTransitionMode = true;
            }
        }
        else if (clb.Equals("deleteNode"))
        {
            bool clickedOnWindows = false;
            int selectIndex = -1;

            for (int i = 0; i < windows.Count; ++i)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selectIndex = i;
                    clickedOnWindows = true;
                    break;
                }
            }

            if (clickedOnWindows)
            {
                BaseNode selNode = windows[selectIndex];
                windows.RemoveAt(selectIndex);

                foreach (BaseNode n in windows)
                {
                    n.NodeDeleted(selNode);
                }
            }
        }
    }

    public static void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

        for (int i = 0; i < 3; ++i)
        {
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}
