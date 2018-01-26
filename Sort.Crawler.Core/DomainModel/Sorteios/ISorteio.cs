using System;
using System.Collections.Generic;
using Sort.Crawler.Core.DomainModel.Loterias;

namespace Sort.Crawler.Core.DomainModel.Sorteios {
    public interface ISorteio : IEntidade {
        DateTime Data { get; set; }
        ILoteria Loteria { get; }
        IList<Resultado> Resultados { get; set; }
        string Url { get; set; }
    }
}