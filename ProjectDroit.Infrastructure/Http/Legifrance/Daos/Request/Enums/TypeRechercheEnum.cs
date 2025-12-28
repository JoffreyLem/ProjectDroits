using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum TypeRechercheEnum
{
    [EnumMember(Value = "UN_DES_MOTS")]
    UN_DES_MOTS,

    [EnumMember(Value = "EXACTE")]
    EXACTE,

    [EnumMember(Value = "TOUS_LES_MOTS_DANS_UN_CHAMP")]
    TOUS_LES_MOTS_DANS_UN_CHAMP,

    [EnumMember(Value = "AUCUN_DES_MOTS")]
    AUCUN_DES_MOTS,

    [EnumMember(Value = "AUCUNE_CORRESPONDANCE_A_CETTE_EXPRESSION")]
    AUCUNE_CORRESPONDANCE_A_CETTE_EXPRESSION
}