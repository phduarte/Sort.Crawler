using OpenQA.Selenium;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {
    internal class MegaMillionsStrategy : TheLotterStrategy {
        
        public MegaMillionsStrategy(IWebDriver driver) : base(driver) {
        }

        public override string ToString() {
            return "Mega Millions";
        }
    }
}
