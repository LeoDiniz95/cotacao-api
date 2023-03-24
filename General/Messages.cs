namespace cotacao_api.General
{
    public class Messages
    {
        public static class Errors
        {
            public const string generic = "Ocorreu um erro inesperado";
            public const string required = "Este campo é obrigatório";
            public const string itemNotFound = "Item não encontrado";

            public const string cnpjlength = "O campo CNPJComprador deve ser preenchido com pelo menos 14 caracteres";
            public const string cnpjCrequired = "O campo CNPJComprador é obrigatório";

            public const string cnpjFlength = "O campo CNPJFornecedor deve ser preenchido com pelo menos 14 caracteres";
            public const string cnpjFRequired = "O campo CNPJFornecedor é obrigatório";

            public const string numeroCotacaoRequired = "O campo NumeroCotacao é obrigatório";

            public const string DataCotacaoRequired = "O campo DataCotacao é obrigatório";

            public const string CEPlength = "O campo CEP deve ser preenchido com pelo menos 8 caracteres";
            public const string CEPRequired = "O campo CEP é obrigatório";

            public const string Descricaolength = "O campo Descricao deve ser preenchido";
            public const string DescricaoRequired = "O campo Descricao é obrigatório";

            public const string numeroItemRequired = "O campo NumeroItem é obrigatório";

            public const string QuantidadeRequired = "O campo Quantidade é obrigatório";
        }
    }
}
