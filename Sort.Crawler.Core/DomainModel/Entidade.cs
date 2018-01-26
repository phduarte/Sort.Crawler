namespace Sort.Crawler.Core.DomainModel {
    public abstract class Entidade : IEntidade {
        
        public Identidade Id { get; private set; }

        protected Entidade() {
            Id = Identidade.Criar();
        }

        protected Entidade(Identidade id) {
            Id = id;
        }
    }
}
