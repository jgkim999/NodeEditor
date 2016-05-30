using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Xml;

[Node(false, "Standard/Ai/Root")]
public class AiRootNode : Node
{
    public const string ID = "AiRootNode";
    public override string GetID { get { return ID; } }

    public override Node Create(Vector2 pos)
    {
        AiRootNode node = CreateInstance<AiRootNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 60);
        node.name = "Root";
        node.headColor = Color.yellow;

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        Color oldColor = GUI.contentColor;
        GUI.contentColor = Color.yellow;
        GUILayout.Label("AI Root Node!");
        GUI.contentColor = oldColor;

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
