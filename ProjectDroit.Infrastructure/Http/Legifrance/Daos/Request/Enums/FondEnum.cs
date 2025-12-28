using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum FondEnum
{
    [EnumMember(Value = "JORF")]
    JORF,

    [EnumMember(Value = "CNIL")]
    CNIL,

    [EnumMember(Value = "CETAT")]
    CETAT,

    [EnumMember(Value = "JURI")]
    JURI,

    [EnumMember(Value = "JUFI")]
    JUFI,

    [EnumMember(Value = "CONSTIT")]
    CONSTIT,

    [EnumMember(Value = "KALI")]
    KALI,

    [EnumMember(Value = "CODE_DATE")]
    CODE_DATE,

    [EnumMember(Value = "CODE_ETAT")]
    CODE_ETAT,

    [EnumMember(Value = "LODA_DATE")]
    LODA_DATE,

    [EnumMember(Value = "LODA_ETAT")]
    LODA_ETAT,

    [EnumMember(Value = "ALL")]
    ALL,

    [EnumMember(Value = "CIRC")]
    CIRC,

    [EnumMember(Value = "ACCO")]
    ACCO
}