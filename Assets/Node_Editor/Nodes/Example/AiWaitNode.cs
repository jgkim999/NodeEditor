using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;
using System.Xml;

[Node(false, "Standard/Ai/Wait")]
public class AiWaitNode : Node
{
    public const string ID = "AiWaitNode";
    public override string GetID { get { return ID; } }
    public int waitTime;
    public string waitTimeStr;

    public override Node Create(Vector2 pos)
    {
        AiWaitNode node = CreateInstance<AiWaitNode>();

        node.rect = new Rect(pos.x, pos.y, 200, 70);
        node.name = "Wait";

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Order");
            orderText = GUILayout.TextField(order.ToString());
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Wait time(msec)");
            waitTimeStr = GUILayout.TextField(waitTime.ToString());
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
    }

    protected internal override void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("ai_node");
        base.WriteXml(writer);

        writer.WriteElementString("wait_time", waitTime.ToString());

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
