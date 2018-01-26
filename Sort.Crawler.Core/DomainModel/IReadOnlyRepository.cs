using System;
using System.Collections.Generic;

namespace Sort.Crawler.Core.DomainModel {
    public interface IReadOnlyRepository<T> where T: IEntidade {
        T Get(Identidade id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> All();
    }
}
