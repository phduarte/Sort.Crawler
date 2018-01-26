using OpenQA.Selenium;
using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal class MassLotteryStrategy : ColetorBase {

        IWebDriver _driver;

        public override event SorteioEncontrado QuandoEncontrar;

        public MassLotteryStrategy(IWebDriver driver) {
            _driver = driver;
        }

        public override IEnumerable<ISorteio> BuscarSorteios(ILoteria premio) {

            DateTime data = premio.Desde;
            
            do {

                var url = $"{premio.Url}&mode=1&year={data.Year}&month={data.Month}&x=9&y=10";

                _driver.Navigate().GoToUrl(url);

                System.Threading.Thread.Sleep(1000);

                var tabela = _driver.FindElement(By.Id("target-area"));

                foreach (var linha in tabela.FindElements(By.TagName("TR"))) {

                    var campos = linha.FindElements(By.TagName("TD"));

                    if (campos.Count == 6) {
                        var dataSorteio = ConverterData(campos[1].Text);
                        var resultados = ConverterResultados(campos[2].Text);

                        if (premio.Sorteios.Any(x => x.Data.Equals(dataSorteio)))
                            continue;

                        var sorteio = new Sorteio(premio) { Data = dataSorteio, Resultados = resultados, Url = url };
                        
                        yield return sorteio;

                        QuandoEncontrar?.Invoke(sorteio);
                    }
                }

                data = data.AddMonths(1);

            } while (data <= DateTime.Today);
        }

        protected static IList<Resultado> ConverterResultados(string text) {

            var _resultados = new List<Resultado>();

            foreach (var i in text.Split('-')) {
                _resultados.Add(new Resultado(int.Parse(i)));
            }

            return _resultados;
        }

        protected static DateTime ConverterData(string text) {

            var t = text.Split('/');
            var data = string.Format("{0}/{1}/{2}", t[1], t[0], t[2]);

            return DateTime.Parse(data);
        }
    }
}
