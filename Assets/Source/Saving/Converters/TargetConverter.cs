using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class TargetConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Target);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jObject    = (JObject)serializer.Deserialize(reader);
        var targetType = Enum.Parse(typeof(TargetType), jObject["targetType"].ToString());

        var stringTarget = jObject["target"].ToString();
        return targetType switch
        {
            TargetType.Direction => Target.Direction(JsonConvert.DeserializeObject<Vector2>(stringTarget)),
            TargetType.Position  => Target.Position(JsonConvert.DeserializeObject<Vector2Int>(stringTarget)),
            TargetType.Entity    => Target.Entity(long.Parse(stringTarget)),
            _                    => throw new ArgumentOutOfRangeException(nameof(targetType))
        };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var v = (Target)value;
        writer.WriteStartObject();
        writer.WritePropertyName("targetType");
        writer.WriteValue(v.targetType);
        writer.WritePropertyName("target");
        
        switch (v.targetType)
        {
            case TargetType.Direction: serializer.Serialize(writer, v.direction); break;
            case TargetType.Position:  serializer.Serialize(writer, v.position); break; 
            case TargetType.Entity:    serializer.Serialize(writer, v.entityID); break;
            default:                   throw new ArgumentOutOfRangeException(nameof(v.targetType));
        }
        writer.WriteEndObject();
    }
}