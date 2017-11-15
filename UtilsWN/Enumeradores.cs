using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace UtilsWN
{
    public static class Enumeradores
    {
        /// <summary>
        /// Obtém a descrição de um determinado Enumerador.
        /// </summary>
        /// <param name="valor">Enumerador que terá a descrição obtida</param>
        /// <returns>String com a descrição do Enumerador</returns>
        /// <remarks></remarks>
        public static string ObterDescricao(Enum valor)
        {
            FieldInfo info = valor.GetType().GetField(valor.ToString());
            DescriptionAttribute[] atributos = (DescriptionAttribute[]) info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return atributos.Length > 0 ? (atributos[0].Description == null) ? "Nulo" : atributos[0].Description : valor.ToString();
        }

        public static IList Listar(Type tipo)
        {
            ArrayList lista = new ArrayList();
            if (tipo != null)
            {
                Array enumValores = Enum.GetValues(tipo);
                foreach (Enum valor in enumValores)
                {
                    lista.Add(new KeyValuePair<Enum, String>(valor, ObterDescricao(valor)));
                }
            }
            return lista;
        }
    }
}
