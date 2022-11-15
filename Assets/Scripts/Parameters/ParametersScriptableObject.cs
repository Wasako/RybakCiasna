using UnityEngine;


[CreateAssetMenu(fileName = "ParametersScriptableObject")]
public class ParametersScriptableObject : ScriptableObject
{
    [EnumNamedArray( typeof(Parameter.Types) )]
    public Parameter[] Parameters = new Parameter[(int)Parameter.Types.Total];
    public void Reset()
    {
        foreach (var parameter in Parameters)
            parameter.Reset();
    }
}
