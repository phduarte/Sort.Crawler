using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Ionic.Zip;
using HtmlAgilityPack;
using System.Linq;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal class CaixaEconomicaStrategy : ColetorBase {

        const string FILE_NAME = "caixa.zip";

        public override event SorteioEncontrado QuandoEncontrar;

        public override IEnumerable<ISorteio> BuscarSorteios(ILoteria premio) {

            Console.Title = premio.Nome;
            string url = premio.Url;

            LimparArquivos();
            Baixar(url, FILE_NAME);
            Descompactar(FILE_NAME);
            
            var doc = new HtmlDocument();

            string html_file_name = Directory.GetFiles("unzip").First(x => x.EndsWith("HTM"));

            using (var file = new StreamReader(html_file_name)) {
                doc.LoadHtml(file.ReadToEnd());
            }

            var linhas = doc.DocumentNode.SelectNodes("/html/body/table/tr");

            foreach (var linha in linhas) {

                var colunaData = linha.SelectNodes("td[2]")?[0].InnerText;
                
                if (colunaData == null)
                    continue;
                
                if(!DateTime.TryParse(colunaData, out DateTime data)) {
                    continue;
                }

                if (data < premio.Desde) {
                    continue;
                }

                var resultados = new List<Resultado>();

                for (int c = 0; c < premio.QuantidadeDeBolas; c++) {
                    var colunaDezena = linha.SelectNodes($"td[{c+3}]")?[0].InnerText;

                    if(int.TryParse(colunaDezena, out int n)) {
                        resultados.Add(new Resultado(n));
                    }
                }

                var sorteio = new Sorteio(premio) { Data = data, Resultados = resultados, Url = premio.Url };

                yield return sorteio;

                QuandoEncontrar?.Invoke(sorteio);
            }

            LimparArquivos();
        }

        private void LimparArquivos() {

            if(File.Exists(FILE_NAME)) {
                File.Delete(FILE_NAME);
            }

            if(Directory.Exists("unzip")) {
                try {
                    Directory.Delete("unzip", true);
                } catch(IOException ex){
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Descompactar(string arquivo) {

            var zip = ZipFile.Read(arquivo);
            zip.ExtractAll("unzip", ExtractExistingFileAction.OverwriteSilently);

        }

        private static void Baixar(string url, string destino) {
            using (HttpClient client = new HttpClient()) {

                var urlBase = new Uri(url).AbsoluteUri;

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, urlBase);

                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");
                requestMessage.Headers.Add("Accept", "application/x-zip-compressed");

                var response = client.SendAsync(requestMessage).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException();

                var resultado = response.Content.ReadAsByteArrayAsync().Result;

                using (var file = new FileStream(destino, FileMode.Create)) {

                    using (var bw = new BinaryWriter(file)) {
                        bw.Write(resultado);
                    }
                }
            }
        }
    }
}
