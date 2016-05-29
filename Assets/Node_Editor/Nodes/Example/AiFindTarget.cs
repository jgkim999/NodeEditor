using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[Node(false, "Standard/Ai/FindTarget Node")]
public class AiFindTargetNode : Node
{
    public const string ID = "AiFindTargetNode";
    public override string GetID { get { return ID; } }
    public int order = 0;
    public string orderText;

    public override Node Create(Vector2 pos)
    {
        AiFindTargetNode node = CreateInstance<AiFindTargetNode>();

        node.rect = new Rect(pos.x, pos.y, 150, 60);
        node.name = "Ai Find Target Node";
        node.headColor = Color.magenta;

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        Color oldColor = GUI.contentColor;
        GUI.contentColor = Color.yellow;
        GUILayout.Label("AI find target Node!");
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
