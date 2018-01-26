using OpenQA.Selenium;
using Sort.Crawler.Core.DomainModel.Loterias;
using System;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    public class ColetorFactory {

        public static IColetorStrategy Create(IWebDriver driver, string premio) {
            switch (premio) {
                case "PowerBall":
                    return new PowerBallStrategy(driver);
                case "Bicho PTM-RJ":
                case "Bicho PT-RJ":
                case "Bicho PTN-RJ":
                case "Bicho Corujinha":
                case "Bicho Federal":
                case "Bicho SP":
                case "Bicho Recife":
                case "Bicho Lotep":
                case "Bicho Look":
                    return new BichoStrategy(driver);
                case "Mega Millions":
                    return new MegaMillionsStrategy(driver);
                case "Mass Cash":
                    return new MassCashStrategy(driver);
                case "Mega Sena":
                    return new CaixaEconomicaStrategy();
                case "Quina":
                    return new CaixaEconomicaStrategy();
                case "Dupla Sena":
                    return new CaixaEconomicaStrategy();
                case "Timemania":
                    return new CaixaEconomicaStrategy();
                case "Lotomania":
                    return new CaixaEconomicaStrategy();
                case "Lotofacil":
                    return new CaixaEconomicaStrategy();
                default:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}
