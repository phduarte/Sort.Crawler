namespace Sort.Crawler.Core.DomainModel.Loterias {
    internal class LoteriaAtrasadoState : ILoteriaState {
        public bool PodeColetar(ILoteria loteria) {
            return true;
        }

        public bool PodeExportar(ILoteria loteria) {
            return false;
        }

        public override string ToString() {
            return "Atrasado";
        }
    }
}
