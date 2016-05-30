using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Xml;

[Node(false, "Standard/Ai/Skill attack")]
public class AiSkillAttackNode : Node
{
    public const string ID = "AiSkillAttackNode";
    public override string GetID { get { return ID; } }
    public string skillValue = "0";

    public override Node Create(Vector2 pos)
    {
        AiSkillAttackNode node = CreateInstance<AiSkillAttackNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 100);
        node.name = "Skill Attack";
        node.headColor = Color.green;

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("AI skill attack");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Order");
        orderText = GUILayout.TextField(order.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("value");
        skillValue = GUILayout.TextField(skillValue.ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

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

        writer.WriteElementString("skill_value", skillValue.ToString());

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
