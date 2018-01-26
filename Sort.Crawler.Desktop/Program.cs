using Sort.Crawler.Core;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Diagnostics;
using System.IO;

namespace Sort.Crawler.Desktop {
    class Program {
        static void Main(string[] args) {

            var servico = ApplicationServices.Instance;

            servico.OnStatusChanged += status =>  Console.Title = status;
            servico.OnFound += EscreverLog;
            servico.Atualizar();

            Console.Beep();
            Console.Beep();
            //AbrirArquivoDeCache();
        }

        private static void EscreverLog(ISorteio sorteio) {
            Screen.Success($"Coletado resultados do sorteio {sorteio.Data.ToShortDateString()} ({string.Join("-", sorteio.Resultados)})");
        }

        private static void AbrirArquivoDeCache() {
            if (File.Exists("cache.dat")) {
                string diretorio = AppDomain.CurrentDomain.BaseDirectory + "reg.dat";
                Process.Start("notepad", diretorio);
            }
        }
    }
}
