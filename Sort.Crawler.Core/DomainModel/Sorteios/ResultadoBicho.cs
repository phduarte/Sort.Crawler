namespace Sort.Crawler.Core.DomainModel.Sorteios {
    public class ResultadoBicho : Resultado {

        int _numero;

        public ResultadoBicho(int numero) : base(numero) {
            _numero = numero;
        }

        public override string ToString() {
            var valor = "0000" + _numero.ToString();
            return valor.Substring(valor.Length-4,4);
        }
    }
}
