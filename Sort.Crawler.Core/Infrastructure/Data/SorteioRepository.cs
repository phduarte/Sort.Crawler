using Sort.Crawler.Core.DomainModel;
using Sort.Crawler.Core.DomainModel.Loterias;
using Sort.Crawler.Core.DomainModel.Sorteios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sort.Crawler.Core.Infrastructure.Data {
    internal class SorteioRepository : ISorteioRepository {

        object _lock = new object();

        static IList<ISorteio> _cache = new List<ISorteio>();
        static bool done;
        const string DB_FILE = "cache.dat";
        const char DELIMITADOR = '\t';

        public SorteioRepository() {
            LoadCacheAsync();
        }

        public void Add(ISorteio sorteio) {

            _cache.Add(sorteio);

            Task.Factory.StartNew(() => {
                lock (_lock) {

                    using (var arquivo = new StreamWriter(DB_FILE, true)) {

                        string[] campos = new string[2 + sorteio.Resultados.Count()];
                        campos[0] = sorteio.Data.ToShortDateString();
                        campos[1] = sorteio.Loteria.Nome;

                        for (int i = 0; i < sorteio.Resultados.Count(); i++) {
                            campos[2 + i] = sorteio.Resultados.ElementAt(i).Numero.ToString();
                        }

                        arquivo.WriteLine(string.Join(DELIMITADOR.ToString(), campos));
                    }
                }
            });
        }

        public IEnumerable<ISorteio> All() {
            return _cache;
        }

        public IEnumerable<ISorteio> Find(Func<ISorteio, bool> predicate) {
            return _cache.Where(predicate);
        }

        public IEnumerable<ISorteio> FindBy(ILoteria loteria) {
            return _cache.Where(x => x.Loteria.Id == loteria.Id);
        }

        public ISorteio Get(Identidade id) {
            return _cache.First(x => x.Id.Equals(id));
        }

        public ISorteio Last() {

            if (_cache.Count == 0)
                return null;

            return _cache.OrderByDescending(x => x.Data).First();
        }

        public void Remove(Identidade id) {
            _cache.Remove(Get(id));
        }

        public void Save(ISorteio sorteio) {
            var x = Get(sorteio.Id);
            x = sorteio;
        }

        async void LoadCacheAsync() {
            await Task.Factory.StartNew(()=> {

                if (!File.Exists(DB_FILE))
                    return;

                if (done) return;

                done = true;

                lock (_lock) {
                    using (var arquivo = new StreamReader(DB_FILE)) {
                        while (!arquivo.EndOfStream) {
                            var campos = arquivo.ReadLine().Split(DELIMITADOR);
                            var premio = new LoteriaRepository().Find(x => x.Nome.Equals(campos[1])).FirstOrDefault();

                            if (premio == null)
                                break;

                            var sorteio = new Sorteio(premio) {
                                Data = DateTime.Parse(campos[0])
                            };

                            for (int c = 2; c < campos.Count(); c++) {
                                if (int.TryParse(campos[c], out int numero)) {
                                    sorteio.Resultados.Add(new Resultado(numero));
                                }
                            }

                            _cache.Add(sorteio);
                        }
                    }
                }
            });
        }
    }
}
