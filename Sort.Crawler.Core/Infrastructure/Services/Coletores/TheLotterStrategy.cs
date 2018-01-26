using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal class TheLotterStrategy : ColetorBase {
        
        private IWebDriver _driver;

        public override event SorteioEncontrado QuandoEncontrar;

        public TheLotterStrategy(IWebDriver driver) {
            _driver = driver;
        }

        public override IEnumerable<ISorteio> BuscarSorteios(ILoteria premio) {

            string url = $"{premio.Url}?DrawNumber=141056";
            IDictionary<DateTime, string> _urls = new Dictionary<DateTime, string>();

            _driver.Navigate().GoToUrl(url);

            var opcoes = new SelectElement(_driver.FindElement(By.Id("ctl00_ContentPlaceHolderMain_ddlDrawNumber")));
            int buscas = 0;

            foreach (var i in opcoes.Options) {
                
                if (buscas > 30) break;

                var valor = i.GetAttribute("value");
                var data = PegarData(i.Text);

                if(data.Year == 1) {
                    continue;
                }

                if(data < premio.Desde) {
                    continue;
                }

                if (premio.Sorteios.Any(x => x.Data.Equals(data))) {
                    continue;
                }

                buscas++;

                _urls.Add(data, $"{premio.Url}?DrawNumber={valor}");
            }

            int pagina = 0;

            foreach (var u in _urls) {

                _driver.Navigate().GoToUrl(u.Value);

                var balls = _driver.FindElements(By.ClassName("results-ball-regular"));

                if (balls.Count == 0)
                    continue;

                var aditional = _driver.FindElement(By.ClassName("results-ball-additional"));
                var resultados = new List<Resultado>();

                foreach (var i in balls) {
                    if (int.TryParse(i.Text, out int resultado)) {
                        resultados.Add(new Resultado(resultado));
                    }
                }

                if (int.TryParse(aditional.Text, out int adicional)) {
                    resultados.Add(new Resultado(adicional));
                }

                var sorteio = new Sorteio(premio) { Data = u.Key, Resultados = resultados, Url = u.Value };

                yield return sorteio;

                QuandoEncontrar?.Invoke(sorteio);
            }
        }

        protected static DateTime PegarData(string text) {

            string[] data = text.Substring(text.IndexOf('|') + 2, 11).Split(' ');
            int mes = PegarMes(data[1]);

            int.TryParse(data[0], out int dia);
            int.TryParse(data[2], out int ano);

            string novaData = string.Format("{0}/{1}/{2}", dia, mes, ano);

            if (DateTime.TryParse(novaData, out DateTime r)) {
                return r;
            }

            return DateTime.MinValue;
        }
    }
}
