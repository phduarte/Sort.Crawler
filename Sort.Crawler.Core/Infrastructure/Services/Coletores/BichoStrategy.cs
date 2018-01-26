using OpenQA.Selenium;
using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal class BichoStrategy : IColetorStrategy {

        IWebDriver _driver;

        public event SorteioEncontrado QuandoEncontrar;

        public BichoStrategy(IWebDriver driver) {
            _driver = driver;
        }

        public IEnumerable<ISorteio> BuscarSorteios(ILoteria premio) {

            bool temResultado = false;
            int page = 1;

            do {

                DateTime data = DateTime.MinValue;
                var url = $"{premio.Url}/page/{page}/";

                _driver.Navigate().GoToUrl(url);

                var corpo = _driver.FindElement(By.Id("content_body"));
                var posts = corpo.FindElements(By.ClassName("post"));

                foreach(var post in posts) {
                    
                    var titulo = post.FindElement(By.ClassName("main_title")).Text;
                    var resultado = post.FindElement(By.ClassName("resultado")).Text;
                    var numeros = PegarNumeros(resultado);
                    data = PegarData(resultado);

                    if (data.Year == 1) continue;

                    var sorteio = new Sorteio(premio) { Data = data, Resultados = numeros, Url = url };
                    
                    yield return sorteio;

                    QuandoEncontrar?.Invoke(sorteio);
                }

                var botaoProximo = _driver.FindElement(By.ClassName("nextpostslink"));

                if (botaoProximo != null) {
                    temResultado = data >= premio.Desde;
                    page++;
                } else {
                    temResultado = false;
                }
                
            } while (temResultado);
        }

        protected static IList<Resultado> PegarNumeros(string resultado) {

            var resultados = new List<Resultado>();
            var regex = new Regex(@"\s(\d{3}|\d{4})\s");
            var m = regex.Matches(resultado);
            
            for(int i = 0; i < m.Count; i++) {
                if (int.TryParse(m[i].Value, out int n)) {
                    resultados.Add(new ResultadoBicho(n));
                }
            }

            return resultados;
        }

        protected static DateTime PegarData(string resultado) {

            Regex regex = new Regex(@"(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}");

            var r = regex.Matches(resultado);

            if (r.Count == 0)
                return DateTime.MinValue;

            if(DateTime.TryParse(r[0].Value, out DateTime novaData)) {
                return novaData;
            } else {


                return DateTime.MinValue;
            }
        }

        public override string ToString() {
            return "Jogo do Bicho";
        }
    }
}
