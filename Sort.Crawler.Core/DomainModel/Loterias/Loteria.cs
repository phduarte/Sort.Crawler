using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sort.Crawler.Core.DomainModel.Loterias {
    internal class Loteria : Entidade, ILoteria {

        #region fields

        IColetorStrategy _coletor;
        ISorteioRepository _repository;
        IExportadorStrategy _exportador;

        #endregion

        #region properties

        public string Nome { get; set; }
        public string Url { get; set; }
        public ISorteio ProximoSorteio => VerificarProximoSorteio();
        public IList<DayOfWeek> DiasDeSorteio { get; set; }
        public IList<TimeSpan> HorariosDeSorteio { get; set; }
        public IEnumerable<ISorteio> Sorteios { get { return _repository.FindBy(this); } }
        public IList<ISorteio> Pendentes { get; private set; } = new List<ISorteio>();
        public ILoteriaState Estado { get; private set; }
        public DateTime Desde { get; set; }
        public TipoLoteria Tipo { get; set; }
        public int QuantidadeDeBolas { get; set; }

        #endregion

        #region events

        public event SorteioEncontrado QuandoEncontrar;
        public event StatusAlterado QuandoAlterarStatus;

        #endregion

        #region constructors

        public Loteria(ISorteioRepository sorteioRepository) {
            Estado = new LoteriaAtrasadoState();
            _repository = sorteioRepository;
            VerificarProximoSorteio();
            Desde = new DateTime(2017, 1, 1);
        }

        #endregion

        #region public methods

        public void Coletar() {

            if (!Estado.PodeColetar(this))
                throw new Exception("Não é possível iniciar uma busca enquanto o estado da loteria for "+Estado.ToString());

            if (_coletor == null) throw new ColetorNaoDefinidoException();

            Estado = new LoteriaAtualizandoState();

            foreach (var i in _coletor.BuscarSorteios(this)) {
                _repository.Add(i);
                QuandoEncontrar?.Invoke(i);
            }

            Estado = new LoteriaAtualizadoState();
        }

        public async void ExportarAsync(string destino) {

            await Task.Factory.StartNew(() => {
                Exportar(destino);
            });
        }

        public void Exportar(string destino) {

            if (!Estado.PodeExportar(this)) {
                throw new Exception("Não é possível exportar os dados enquanto o estado da loteria for " + Estado.ToString());
            }

            if (_exportador == null)
                throw new ExportadorNaoDefinidoException();

            _exportador.Exportar(this, destino);
        }

        public void DefinirColetor(IColetorStrategy coletorStrategy) {
            _coletor = coletorStrategy;
            _coletor.QuandoEncontrar += QuandoEncontrar;
        }

        public void DefinirExportador(IExportadorStrategy exportadorStrategy) {
            _exportador = exportadorStrategy;
        }

        #endregion

        #region private methods

        Sorteio VerificarProximoSorteio() {

            var proximaData = new DateTime(2017, 1, 1);
            var dataReferencia = _repository.Last()?.Data;

            if (dataReferencia.HasValue)
                proximaData = dataReferencia.Value;

            var dia = DateTime.Now.DayOfWeek;
            var hora = DateTime.Now.Hour;

            return new Sorteio(this) { Data = proximaData};
        }

        #endregion

        #region overrided methods

        public override string ToString() {
            return Nome;
        }

        #endregion
    }
}
