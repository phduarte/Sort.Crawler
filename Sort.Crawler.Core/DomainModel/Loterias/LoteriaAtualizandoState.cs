namespace Sort.Crawler.Core.DomainModel.Loterias {
    internal class LoteriaAtualizandoState : ILoteriaState {
        public bool PodeColetar(ILoteria loteria) {
            return false;
        }

        public bool PodeExportar(ILoteria loteria) {
            return false;
        }

        public override string ToString() {
            return "Atualizando";
        }
    }
}
