namespace Sort.Crawler.Core.DomainModel.Loterias {
    public interface ILoteriaState {
        bool PodeColetar(ILoteria loteria);
        bool PodeExportar(ILoteria loteria);
    }
}