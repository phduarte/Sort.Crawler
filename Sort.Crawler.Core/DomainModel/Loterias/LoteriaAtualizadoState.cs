namespace Sort.Crawler.Core.DomainModel.Loterias {
    internal class LoteriaAtualizadoState : ILoteriaState {
        public bool PodeColetar(ILoteria loteria) {
            return false;
        }

        public bool PodeExportar(ILoteria loteria) {
            return true;
        }

        public override string ToString() {
            return "Atualizado";
        }
    }
}
