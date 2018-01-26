using Sort.Crawler.Core.DomainModel;
using Sort.Crawler.Core.DomainModel.Loterias;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sort.Crawler.Core.Infrastructure.Data {
    internal class LoteriaRepository : ILoteriaRepository {

        static IList<Loteria> _cache = LoadCache();

        public LoteriaRepository() {
        }

        public IEnumerable<ILoteria> All() {
            return _cache;
        }

        public IEnumerable<ILoteria> Find(Func<ILoteria, bool> predicate) {
            return _cache.Where(predicate);
        }

        public ILoteria Get(Identidade id) {
            return _cache.First(x => x.Id.Equals(id));
        }

        public ILoteria Next() {
            return WaitingList().First();
        }

        public IEnumerable<ILoteria> WaitingList() {
            return _cache;
        }

        static IList<Loteria> LoadCache() {

            var sorteioRepository = new SorteioRepository();

            var lista = new List<Loteria>{
                    new Loteria(sorteioRepository) {
                        Nome = "Mass Cash",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://www.masslottery.com/games/lottery/search/results-lottery.html?game_id=12",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 5,
                        Tipo = TipoLoteria.Combinada
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho PTM-RJ",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/11-horas/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 7
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho PT-RJ",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/14-horas/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 7
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho PTN-RJ",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/18-horas/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 7
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho Corujinha",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/corujinha/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 7
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho Federal",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/federal/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 10
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho SP",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/sao-paulo/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 7
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho Recife",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/loteria-popular/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 5
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho Lotep",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/lotep/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 5
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Bicho Look",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"http://resultadodojogodobicho.deunopostehoje.com/look-loterias/",
                        Desde = DateTime.Today.AddMonths(-2),
                        Tipo = TipoLoteria.Permutada,
                        QuantidadeDeBolas = 5
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "PowerBall",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan> { new TimeSpan(20, 0, 0) },
                        Url = @"https://www.thelotter.com/pt/resultados-loteria/powerball-eua/",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 6,
                        Tipo = TipoLoteria.Mista
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Mega Millions",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"https://www.thelotter.com/pt/resultados-loteria/mega-millions-eua/",
                        Desde = new DateTime(2017, 1, 1),
                        Tipo = TipoLoteria.Mista,
                        QuantidadeDeBolas = 6
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Mega Sena",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_megase.zip",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 6,
                        Tipo = TipoLoteria.Combinada
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Quina",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_quina.zip",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 5,
                        Tipo = TipoLoteria.Combinada
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Dupla Sena",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"http://www1.caixa.gov.br/loterias/_arquivos/loterias/d_dplsen.zip",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 6,
                        Tipo = TipoLoteria.Combinada
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Timemania",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_timema.zip",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 7,
                        Tipo = TipoLoteria.Combinada
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Lotomania",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_lotoma.zip",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 20,
                        Tipo = TipoLoteria.Combinada
                    },
                    new Loteria(sorteioRepository) {
                        Nome = "Lotofacil",
                        DiasDeSorteio = new List<DayOfWeek> { { DayOfWeek.Wednesday }, { DayOfWeek.Saturday } },
                        HorariosDeSorteio = new List<TimeSpan>(),
                        Url = @"http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_lotfac.zip",
                        Desde = DateTime.Today.AddMonths(-2),
                        QuantidadeDeBolas = 15,
                        Tipo = TipoLoteria.Combinada
                    }
            };

            return lista;
        }
    }
}
