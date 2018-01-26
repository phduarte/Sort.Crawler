using System.Collections.Generic;

namespace Sort.Crawler.Core.DomainModel.Loterias {

    internal class LoteriaServices {

        ILoteriaRepository _premioRepository;

        public LoteriaServices(ILoteriaRepository premioRepository) {
            _premioRepository = premioRepository;
        }

        public ILoteria BuscarProximo() {
            return _premioRepository.Next();
        }

        public IEnumerable<ILoteria> BuscarPendentes() {
            return _premioRepository.WaitingList();
        }
    }
}
