﻿using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Xml;

[Node(false, "Standard/Ai/FindTarget")]
public class AiFindTargetNode : Node
{
    public const string ID = "AiFindTargetNode";
    public override string GetID { get { return ID; } }

    public override Node Create(Vector2 pos)
    {
        AiFindTargetNode node = CreateInstance<AiFindTargetNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 60);
        node.name = "Find Target";
        node.headColor = Color.magenta;

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("AI find target Node!");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Order");
        orderText = GUILayout.TextField(order.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        Inputs[0].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        Outputs[0].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    protected internal override void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("ai_node");
        base.WriteXml(writer);
        writer.WriteEndElement();
    }

    public override bool Calculate()
    {
        if (!allInputsReady())
            return false;
        Outputs[0].SetValue<float>(Inputs[0].GetValue<float>() * 5);
        return true;
    }
}
