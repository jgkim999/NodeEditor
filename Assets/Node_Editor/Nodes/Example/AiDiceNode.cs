using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Xml;

[Node(false, "Standard/Ai/Dice")]
public class AiDiceNode : Node
{
    public const string ID = "AiDiceNode";
    public override string GetID { get { return ID; } }
    public enum DiceResultType { True, False }
    public DiceResultType diceResult = DiceResultType.False;

    public override Node Create(Vector2 pos)
    {
        AiDiceNode node = CreateInstance<AiDiceNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 100);
        node.name = "Dice";
        node.headColor = new Color(251.0f / 255.0f, 227.0f / 255.0f, 228.0f / 255.0f, 0.9f);

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("AI dice Node!");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Order");
        orderText = GUILayout.TextField(order.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Result");
        diceResult = (DiceResultType) UnityEditor.EditorGUILayout.EnumPopup(diceResult);
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

        writer.WriteElementString("dice_result", diceResult.ToString());

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
