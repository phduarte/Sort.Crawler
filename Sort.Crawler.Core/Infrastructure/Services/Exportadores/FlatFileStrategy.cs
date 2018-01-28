using Sort.Crawler.Core.DomainModel.Loterias;
using System;
using System.Diagnostics;
using System.IO;

namespace Sort.Crawler.Core.Infrastructure.Services.Exportadores {
    internal class FlatFileStrategy : IExportadorStrategy {

        const string FOLDER_NAME = "text";
        const string DELIMITADOR = "\t";

        public void Exportar(ILoteria loteria, string destino) {

            if (!Directory.Exists(FOLDER_NAME)) {
                Directory.CreateDirectory(FOLDER_NAME);
            }
            
            using (var arquivo  = new StreamWriter($"{FOLDER_NAME}\\{destino}.txt")) {

                var campos = new string[loteria.QuantidadeDeBolas + 2];
                
                campos[0] = "Loteria";
                campos[1] = "Data";

                for(int c = 0; c < loteria.QuantidadeDeBolas; c++) {
                    campos[c+2] = $"Bola {c+1}";
                }

                arquivo.WriteLine(string.Join(DELIMITADOR,campos));

                foreach(var i in loteria.Sorteios) {

                    Array.Clear(campos, 0, loteria.QuantidadeDeBolas + 2);

                    campos[0] = loteria.Nome;
                    campos[1] = i.Data.ToShortDateString();

                    for(int c = 0; c < loteria.QuantidadeDeBolas; c++) {
                        try {
                            campos[c + 2] = i.Resultados[c].ToString();
                        } catch(ArgumentOutOfRangeException ex){
                            Debug.WriteLine($"Erro em FlatFileStrategy. Loteria {loteria.Nome}. Erro: A loteria pode ter enviado menos números do que a quantidade esperada. Descrição: {ex.StackTrace}");
                        } catch(Exception ex) {
                            Debug.WriteLine($"Erro em FlatFileStrategy. Loteria {loteria.Nome}. Erro: {ex.Message} Descrição: {ex.StackTrace}");
                        }
                    }

                    arquivo.WriteLine(string.Join(DELIMITADOR, campos));
                }
            }
        }
    }
}
