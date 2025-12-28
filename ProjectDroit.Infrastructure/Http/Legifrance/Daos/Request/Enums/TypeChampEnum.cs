using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum TypeChampEnum
{
    [EnumMember(Value = "ALL")]
    ALL,

    [EnumMember(Value = "TITLE")]
    TITLE,

    [EnumMember(Value = "TABLE")]
    TABLE,

    [EnumMember(Value = "NOR")]
    NOR,

    [EnumMember(Value = "NUM")]
    NUM,

    [EnumMember(Value = "ADVANCED_TEXTE_ID")]
    ADVANCED_TEXTE_ID,

    [EnumMember(Value = "NUM_DELIB")]
    NUM_DELIB,

    [EnumMember(Value = "NUM_DEC")]
    NUM_DEC,

    [EnumMember(Value = "NUM_ARTICLE")]
    NUM_ARTICLE,

    [EnumMember(Value = "ARTICLE")]
    ARTICLE,

    [EnumMember(Value = "MINISTERE")]
    MINISTERE,

    [EnumMember(Value = "VISA")]
    VISA,

    [EnumMember(Value = "NOTICE")]
    NOTICE,

    [EnumMember(Value = "VISA_NOTICE")]
    VISA_NOTICE,

    [EnumMember(Value = "TRAVAUX_PREP")]
    TRAVAUX_PREP,

    [EnumMember(Value = "SIGNATURE")]
    SIGNATURE,

    [EnumMember(Value = "NOTA")]
    NOTA,

    [EnumMember(Value = "NUM_AFFAIRE")]
    NUM_AFFAIRE,

    [EnumMember(Value = "ABSTRATS")]
    ABSTRATS,

    [EnumMember(Value = "RESUMES")]
    RESUMES,

    [EnumMember(Value = "TEXTE")]
    TEXTE,

    [EnumMember(Value = "ECLI")]
    ECLI,

    [EnumMember(Value = "NUM_LOI_DEF")]
    NUM_LOI_DEF,

    [EnumMember(Value = "TYPE_DECISION")]
    TYPE_DECISION,

    [EnumMember(Value = "NUMERO_INTERNE")]
    NUMERO_INTERNE,

    [EnumMember(Value = "REF_PUBLI")]
    REF_PUBLI,

    [EnumMember(Value = "RESUME_CIRC")]
    RESUME_CIRC,

    [EnumMember(Value = "TEXTE_REF")]
    TEXTE_REF,

    [EnumMember(Value = "TITRE_LOI_DEF")]
    TITRE_LOI_DEF,

    [EnumMember(Value = "RAISON_SOCIALE")]
    RAISON_SOCIALE,

    [EnumMember(Value = "MOTS_CLES")]
    MOTS_CLES,

    [EnumMember(Value = "IDCC")]
    IDCC
}