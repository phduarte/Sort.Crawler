using Sort.Crawler.Core.DomainModel.Sorteios;
using System.Collections.Generic;

namespace Sort.Crawler.Core.DomainModel.Loterias {
    public interface IColetorStrategy {
        IEnumerable<ISorteio> BuscarSorteios(ILoteria loteria);
        event SorteioEncontrado QuandoEncontrar;
    }
}