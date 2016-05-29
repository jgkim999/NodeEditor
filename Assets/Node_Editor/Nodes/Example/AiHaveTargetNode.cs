using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[Node(false, "Standard/Ai/HaveTarget")]
public class AiHaveTargetNode : Node
{
    public const string ID = "AiHaveTargetNode";
    public override string GetID { get { return ID; } }
    public int order = 0;
    public string orderText;

    public override Node Create(Vector2 pos)
    {
        AiHaveTargetNode node = CreateInstance<AiHaveTargetNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 60);
        node.name = "Have target";
        node.headColor = Color.magenta;

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("AI have target Node!");

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

    protected internal override void OnAddOutputConnection(NodeOutput output)
    {
        if (output == null)
            return;
        //int outputCount = this.Outputs.Count;
    }

    public override bool Calculate()
    {
        if (!allInputsReady())
            return false;
        Outputs[0].SetValue<float>(Inputs[0].GetValue<float>() * 5);
        return true;
    }
}
