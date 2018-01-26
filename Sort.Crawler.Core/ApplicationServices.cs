using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.Infrastructure.Data;
using Sort.Crawler.Core.Infrastructure.Services.Coletores;
using Sort.Crawler.Core.Infrastructure.Services.Exportadores;
using System;
using System.Collections.Generic;

namespace Sort.Crawler.Core {

    public delegate void StatusAlterado(string status);

    public class ApplicationServices {

        readonly LoteriaServices _premioServices;
        public event SorteioEncontrado OnFound;
        public event StatusAlterado OnStatusChanged;

        //singleton pois essa classe não guarda nenhum estado.
        static ApplicationServices _instance;
        public static ApplicationServices Instance => _instance = _instance ?? new ApplicationServices();
        
        private ApplicationServices() {
            _premioServices = new LoteriaServices(new LoteriaRepository());
        }
        
        public IEnumerable<ILoteria> WaitingList() {
            return _premioServices.BuscarPendentes();
        }

        public void Atualizar() {

            OnStatusChanged?.Invoke("Processo de atualização iniciado.");

            var loterias = WaitingList();

            using (IWebDriver driver = new ChromeDriver()) {

                foreach (var loteria in loterias) {
                    OnStatusChanged?.Invoke($"Coletando dados da loteria {loteria.Nome}");
                    loteria.QuandoEncontrar += OnFound;
                    loteria.DefinirColetor(ColetorFactory.Create(driver, loteria.Nome));
                    loteria.DefinirExportador(ExportadorFactory.Create(TipoDeExportacao.Html));
                    loteria.Coletar();
                    loteria.ExportarAsync(loteria.Nome);
                }

                driver.Quit();
            }

            OnStatusChanged?.Invoke("Processo de atualizado concluído.");
        }
    }
}
