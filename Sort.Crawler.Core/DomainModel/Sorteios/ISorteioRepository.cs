using Sort.Crawler.Core.DomainModel.Loterias;
using System.Collections.Generic;

namespace Sort.Crawler.Core.DomainModel.Sorteios {
    public interface ISorteioRepository : IRepository<ISorteio> {
        IEnumerable<ISorteio> FindBy(ILoteria premio);
        ISorteio Last();
    }
}
