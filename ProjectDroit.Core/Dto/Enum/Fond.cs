using System.Runtime.Serialization;

namespace ProjectDroit.Core.Dto.Enum;

public enum FondApiName
{
    [EnumMember(Value = "CODE")]
    Code,
    [EnumMember(Value = "LEGI")]
    Legi,
    [EnumMember(Value = "KALI")]
    Kali,
    [EnumMember(Value = "JORF")]
    Jorf
}