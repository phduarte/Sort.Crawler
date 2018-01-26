using System;
using System.Runtime.Serialization;

namespace Sort.Crawler.Core.DomainModel.Loterias {
    [Serializable]
    internal class ExportadorNaoDefinidoException : Exception {
        public ExportadorNaoDefinidoException() {
        }

        public ExportadorNaoDefinidoException(string message) : base(message) {
        }

        public ExportadorNaoDefinidoException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ExportadorNaoDefinidoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}