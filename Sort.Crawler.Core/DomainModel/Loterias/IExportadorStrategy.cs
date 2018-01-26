namespace Sort.Crawler.Core.DomainModel.Loterias {
    public interface IExportadorStrategy {
        void Exportar(ILoteria loteria, string destino);
    }
}
