using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;

[Node(false, "Standard/Ai/Dice check")]
public class AiDiceCheckNode : Node
{
    public const string ID = "AiDiceCheckNode";
    public override string GetID { get { return ID; } }
    public int order = 0;
    public string orderText;
    // = equals 1 + 1 = 2
    // ≠ not equal to 1 + 1 ≠ 1
    // > greater than 5 > 2
    // < less than 7 < 9
    // ≥ greater than or equal to marbles ≥ 1
    // ≤ less than or equal to dogs ≤ 3
    public enum CompareType { Equal, NotEqual, GreaterThan, LessThan, GreaterThanOrEqual, LessThanOrEqual }
    public CompareType compType = CompareType.GreaterThanOrEqual;
    public string checkValue = "0";

    public override Node Create(Vector2 pos)
    {
        AiDiceCheckNode node = CreateInstance<AiDiceCheckNode>();

        node.rect = new Rect(pos.x, pos.y, 200, 100);
        node.name = "Dice check";
        node.headColor = new Color(251.0f/255.0f, 227.0f/255.0f, 228.0f/255.0f, 0.9f);

        node.CreateInput("Value", "Float");
        node.CreateOutput("Output val", "Float");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("AI dice check node!");

        GUILayout.BeginVertical();
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Order");
            orderText = GUILayout.TextField(order.ToString());
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
#if UNITY_EDITOR
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Comp");
            compType = (CompareType) UnityEditor.EditorGUILayout.EnumPopup(compType);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
#endif
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("value");
            checkValue = GUILayout.TextField(checkValue.ToString());
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();


//             GUILayout.BeginVertical();
//             Inputs[0].DisplayLayout();
//             GUILayout.EndVertical();

            //             GUILayout.BeginVertical();
            //             Outputs[0].DisplayLayout();
            //             GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
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
