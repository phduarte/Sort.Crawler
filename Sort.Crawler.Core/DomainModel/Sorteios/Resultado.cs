namespace Sort.Crawler.Core.DomainModel.Sorteios {
    public class Resultado {
        public int Numero { get; private set; }

        public Resultado(int numero) {
            Numero = numero;
        }

        public override string ToString() {
            return Numero.ToString();
        }
    }
}
