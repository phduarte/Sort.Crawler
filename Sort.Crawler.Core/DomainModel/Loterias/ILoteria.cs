using System;
using System.Collections.Generic;
using Sort.Crawler.Core.DomainModel.Sorteios;

namespace Sort.Crawler.Core.DomainModel.Loterias {
    public interface ILoteria : IEntidade {
        DateTime Desde { get; set; }
        IList<DayOfWeek> DiasDeSorteio { get; set; }
        ILoteriaState Estado { get; }
        IList<TimeSpan> HorariosDeSorteio { get; set; }
        string Nome { get; set; }
        IList<ISorteio> Pendentes { get; }
        ISorteio ProximoSorteio { get; }
        int QuantidadeDeBolas { get; set; }
        IEnumerable<ISorteio> Sorteios { get; }
        TipoLoteria Tipo { get; set; }
        string Url { get; set; }

        event StatusAlterado QuandoAlterarStatus;
        event SorteioEncontrado QuandoEncontrar;

        void Coletar();
        void DefinirColetor(IColetorStrategy coletorStrategy);
        void DefinirExportador(IExportadorStrategy exportadorStrategy);
        void Exportar(string destino);
        void ExportarAsync(string destino);
    }
}