using System.ComponentModel;

namespace UtilsWN.FAC
{
    /// <summary>
    /// Enumerador com as opções de contratos FAC
    /// </summary>
    /// <remarks></remarks>
    /// 
    public enum Contrato
    { 
        [Description("Associação Comercial de São Paulo")] ACSP,
        [Description("Banco do Brasil")] BB,
        [Description("Bradesco")] BRADESCO,
        [Description("Boa Vista Serviços")] BVS,
        [Description("DmCard")] DMCARD,
        [Description("Hoepers")] HOEPERS,
        [Description("Lopes Supermercados")] LOPES,
        [Description("OMNI")] OMNI,
        [Description("Oscar Calçados")] OSCAR,
        [Description("Jô Calçados")] JOCALCADOS,
        [Description("Reaval")] REAVAL,
        [Description("Zanc")] ZANC
    }

    public enum Processamento : byte
    { 
        [Description("Produção")] PRODUCAO = 1,
        [Description("Teste")] TESTE = 0
    }

    public enum Tipo
    { 
        [Description("Simples")] SIMPLES,
        [Description("Registrado")] REGISTRADO,
        [Description("Registrado Com AR")] REGISTRADO_COM_AR
    }
}
