using OpenQA.Selenium;

namespace Sort.Crawler.Core.Infrastructure.Services.Coletores {

    internal class PowerBallStrategy : TheLotterStrategy {
        
        public PowerBallStrategy(IWebDriver driver) : base(driver) {
        }
        
        public override string ToString() {
            return "PowerBall";
        }
    }
}
