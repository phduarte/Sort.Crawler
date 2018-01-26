namespace Sort.Crawler.Core.DomainModel {
    public interface IRepository<T> : IReadOnlyRepository<T> where T: IEntidade {
        void Add(T entity);
        void Remove(Identidade id);
        void Save(T entity);
    }
}
