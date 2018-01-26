using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal abstract class ColetorBase : IColetorStrategy {

        public abstract event SorteioEncontrado QuandoEncontrar;

        public abstract IEnumerable<ISorteio> BuscarSorteios(ILoteria premio);

        protected static string GetHtml(string url) {

            using (HttpClient client = new HttpClient()) {

                var urlBase = new Uri(url).AbsoluteUri;

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, urlBase);

                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");
                requestMessage.Headers.Add("Accept", "text/html");

                var response = client.SendAsync(requestMessage).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException();

                return response.Content.ReadAsStringAsync().Result;
            }

        }

        protected static int PegarMes(string mes) {
            switch (mes) {
                case "jan": return 1;
                case "fev": case "feb": return 2;
                case "mar": return 3;
                case "abr": case "apr": return 4;
                case "mai": case "may": return 5;
                case "jun": return 6;
                case "jul": return 7;
                case "ago": case "aug": return 8;
                case "set": case "sep": return 9;
                case "out": case "oct": return 10;
                case "nov": return 11;
                case "dez": case "dec": return 12;
            }

            return 0;
        }
    }
}