using Sort.Crawler.Core.DomainModel.Loterias;
using System.IO;
using System.Linq;

namespace Sort.Crawler.Core.Infrastructure.Services.Exportadores {
    internal class HtmlStrategy : IExportadorStrategy {

        const string FOLDER_NAME = "html";

        public void Exportar(ILoteria loteria, string fileName) {

            if (!Directory.Exists(FOLDER_NAME)) {
                Directory.CreateDirectory(FOLDER_NAME);
            }

            using(var arquivo = new StreamWriter($"{FOLDER_NAME}\\{fileName}.html")) {
                string cabecalho = GerarCabecalho(loteria.QuantidadeDeBolas);

                string corpo = @"<html><head><title>Resultado @nome</title><style>TD {FONT-FAMILY: Arial;FONT-SIZE: 10pt;HEIGHT: 15pt;TEXT-ALIGN: center}
                        </style></head><body>
                        <p><strong><big><big><font face=Arial color=#004080>Resultado @nome</font></big></big></strong></p>
                                <p><img src=t2.gif></p>
                            <table border=0 cellspacing=1 cellpadding=0 width=1810>@cabecalho@conteudo</table></body></html>";


                string conteudo = string.Empty;
                int u = 1;

                foreach (var sorteio in loteria.Sorteios.OrderBy(x=>x.Data)) {

                    string linha = $"<td>{u}</td><td>{sorteio.Data.ToShortDateString()}</td>";

                    var bolas = sorteio.Resultados.Take(loteria.QuantidadeDeBolas);
                    
                    if(loteria.Tipo == TipoLoteria.Combinada) {
                        bolas = bolas.OrderBy(x => x.Numero).ToList();
                    }

                    foreach (var bola in bolas) {
                        linha += $"<td>{bola}</td>";
                    }

                    //inclui colunas adicionais sem dado
                    for (int c = 0; c < 13; c++) {
                        linha += "<td>-</td>";
                    }

                    conteudo += u++ % 2 == 0 ? $"<tr bgcolor=#D9E6F4>{linha}</tr>" : $"<tr>{linha}</tr>";
                }

                corpo = corpo.Replace("@nome", loteria.Nome);
                corpo = corpo.Replace("@cabecalho", cabecalho);
                corpo = corpo.Replace("@conteudo", conteudo);

                arquivo.Write(corpo);

            }
        }

        private static string GerarCabecalho(int bolas) {

            string conteudo =  @"<tr>
                                    <th width=50  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Concurso</font></small></th>
                                    <th width=100 height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Data Sorteio</font></small></th>
                                    @linha
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Arrecadacao_Total</font></small></th>
                                    <th width=95 height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Ganhadores_Sena</font></small></th>
                                    <th width=95 height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Cidade</font></small></th>
                                    <th width=95 height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>UF</font></small></th>
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Rateio_Sena</font></small></th>
                                    <th width=131 height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Ganhadores_Quina</font></small></th>
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Rateio_Quina</font></small></th>
                                    <th width=131  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Ganhadores_Quadra</font></small></th>
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Rateio_Quadra</font></small></th>
                                    <th width=70  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Acumulado</font></small></th>
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Valor_Acumulado</font></small></th>
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Estimativa_Prêmio</font></small></th>
                                    <th width=120  height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>Acumulado_Mega_da_Virada</font></small></th>
                                    </tr>";

            string linha = string.Empty;

            for(int c = 0; c < bolas; c++) {
                linha += $"<th width=80 height=20 bgcolor=#7BA8D9><small><font face=Arial color=#FFFFFF>{c + 1}ª Dezena</font></small></th>";
            }

            conteudo = conteudo.Replace("@linha", linha);

            return conteudo;
        }
    }
}
