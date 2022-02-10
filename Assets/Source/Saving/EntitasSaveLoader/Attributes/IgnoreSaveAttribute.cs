using System;

/// <summary>
/// with this attribute, class will not be saved and deserialized by saveLoader
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IgnoreSaveAttribute : Attribute
{
}