using System;
using System.Runtime.Serialization;

namespace Sort.Crawler.Core.DomainModel.Loterias {
    [Serializable]
    internal class ColetorNaoDefinidoException : Exception {
        public ColetorNaoDefinidoException() {
        }

        public ColetorNaoDefinidoException(string message) : base(message) {
        }

        public ColetorNaoDefinidoException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ColetorNaoDefinidoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}