using System;
using GraphProcessor;

public class ObjectParameter : ExposedParameter
{
    public override object value { get; set; }

    public override Type GetValueType() => typeof(object);
}