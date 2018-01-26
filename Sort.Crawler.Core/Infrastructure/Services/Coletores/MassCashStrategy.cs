using OpenQA.Selenium;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal class MassCashStrategy : MassLotteryStrategy {

        public MassCashStrategy(IWebDriver driver) : base(driver) {
        }

        public override string ToString() {
            return "Mass Cash";
        }
    }
}
