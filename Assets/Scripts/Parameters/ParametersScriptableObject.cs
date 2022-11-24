using UnityEngine;


[CreateAssetMenu(fileName = "ParametersScriptableObject")]
public class ParametersScriptableObject : ScriptableObject
{
    [EnumNamedArray( typeof(Parameter.Types) )]
    public Parameter[] Parameters = new Parameter[(int)Parameter.Types.Total];

    // Resets all parameters to their default values
    public void Reset()
    {
        foreach (var parameter in Parameters)
            parameter.Reset();
    }

    private void OnValidate()
    {
        if (Parameters.Length != (int)Parameter.Types.Total)
            System.Array.Resize(ref Parameters, (int)Parameter.Types.Total);
    }
}
