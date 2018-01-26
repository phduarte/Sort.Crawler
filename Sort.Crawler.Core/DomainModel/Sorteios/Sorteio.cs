using Sort.Crawler.Core.DomainModel.Loterias;
using System;
using System.Collections.Generic;

namespace Sort.Crawler.Core.DomainModel.Sorteios {
    internal class Sorteio : Entidade, ISorteio {

        #region properties

        public DateTime Data { get; set; }
        public IList<Resultado> Resultados { get; set; } = new List<Resultado>();
        public ILoteria Loteria { get; private set; }
        public string Url { get; set; }

        #endregion

        public Sorteio(Identidade id) : base(id) {
        }

        public Sorteio(ILoteria loteria) {
            Loteria = loteria;
        }

        public Sorteio(Identidade id, ILoteria loteria) : base(id) {
            Loteria = loteria;
        }

        public override string ToString() {
            var resultado = string.Join("-", Resultados);
            return $"{Loteria.Nome} : {Data.ToShortDateString()} - {resultado}";
        }
    }
}
