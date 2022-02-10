using System;
using GraphProcessor;

[Serializable]
public class MatrixParameter : ExposedParameter
{
    private Matrix _val;

    public override object value
    {
        get => _val;
        set => _val = (Matrix)value;
    }

    public override Type GetValueType() => typeof(Matrix);
}