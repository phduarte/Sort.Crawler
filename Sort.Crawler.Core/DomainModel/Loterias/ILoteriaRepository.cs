using System.Collections.Generic;

namespace Sort.Crawler.Core.DomainModel.Loterias {
    public interface ILoteriaRepository : IReadOnlyRepository<ILoteria> {
        ILoteria Next();
        IEnumerable<ILoteria> WaitingList();
    }
}
