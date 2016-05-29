using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[Node(false, "Standard/Ai/AiSequence Node")]
public class AiSequenceNode : Node
{
    public const string ID = "AiSequenceNode";
    public override string GetID { get { return ID; } }
    public int order = 0;
    public string orderText;

    public override Node Create(Vector2 pos)
    {
        AiSequenceNode node = CreateInstance<AiSequenceNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 60);
        node.name = "Ai Sequence Node";
        node.headColor = Color.cyan;

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("AI Sequence Node!");

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

    protected internal override void OnAddInputConnection(NodeInput input)
    {
        if (input == null)
            return;
        order = input.connection.connections.Count;
    }

    public override bool Calculate()
    {
        if (!allInputsReady())
            return false;
        Outputs[0].SetValue<float>(Inputs[0].GetValue<float>() * 5);
        return true;
    }
}
