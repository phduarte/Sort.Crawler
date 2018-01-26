namespace Sort.Crawler.Core.DomainModel.Loterias {
    public enum TipoLoteria {
        /// <summary>
        /// A ordem das bolas não influenciam.
        /// </summary>
        Combinada,
        /// <summary>
        /// Esse tipo de loteria não pode ordenar as bolas
        /// </summary>
        Permutada,

        Mista
    }
}
