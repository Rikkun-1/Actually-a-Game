using System;
using GraphProcessor;

[Serializable]
public class ObjectParameter : ExposedParameter
{
    public override object value { get; set; }

    public override Type GetValueType() => typeof(object);
}