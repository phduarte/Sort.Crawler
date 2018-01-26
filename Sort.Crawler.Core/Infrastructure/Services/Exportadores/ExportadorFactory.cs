using Sort.Crawler.Core.DomainModel.Loterias;
using System;

namespace Sort.Crawler.Core.Infrastructure.Services.Exportadores {
    public static class ExportadorFactory {

        public static IExportadorStrategy Create(TipoDeExportacao tipo) {
            switch (tipo) {
                case TipoDeExportacao.Html: return new HtmlStrategy();
                case TipoDeExportacao.FlatFile: return new FlatFileStrategy();
                default:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}
