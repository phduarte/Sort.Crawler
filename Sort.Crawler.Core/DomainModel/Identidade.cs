using System;

namespace Sort.Crawler.Core.DomainModel {
    public struct Identidade {

        string _id;

        public Identidade(string id) {
            _id = id;
        }

        public static Identidade Criar() {
            return new Identidade(Guid.NewGuid().ToString());
        }

        public override bool Equals(object obj) {
            if (obj is Identidade i) {
                return _id.Equals(i.ToString());
            }
            return false;
        }

        public override int GetHashCode() {
            return _id.GetHashCode();
        }


        public static bool operator ==(Identidade a, Identidade b) {
            return a.Equals(b);
        }

        public static bool operator !=(Identidade a, Identidade b) {
            return !a.Equals(b);
        }

        public override string ToString() {
            return _id;
        }
    }
}
