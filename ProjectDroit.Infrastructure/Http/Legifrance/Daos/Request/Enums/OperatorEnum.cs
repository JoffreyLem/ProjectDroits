using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum OperatorEnum
{
    [EnumMember(Value = "ET")]
    ET,

    [EnumMember(Value = "OU")]
    OU
}