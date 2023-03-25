using cotacao_api.Data;
using cotacao_api.General;
using cotacao_api.Models;

namespace cotacao_api.Repositories
{
    public class Cotacao
    {
        private DataContext _context { get; }
        public Cotacao(DataContext context)
        {
            _context = context;
        }

        public GeneralResult GetAll()
        {
            var result = new GeneralResult();
            var cotacaoItems = new CotacaoItem(_context);

            try
            {
                var cotacoes = _context.CotacaoDMs?.Where(x => x.Status == Constants.Status.active);
                List<object> cotacaoResponse = new List<object>();

                foreach (var cotacao in cotacoes)
                {
                    var objeto = new
                    {
                        cotacao,
                        CotacaoItens = cotacaoItems.GetByCotacao(cotacao.Id)
                    };

                    cotacaoResponse.Add(objeto);
                }

                result.data = cotacaoResponse;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public CotacaoDM Get(int id) => _context.CotacaoDMs?.SingleOrDefault(x => x.Id == id && x.Status == Constants.Status.active);

        public GeneralResult GetAddress(string cep)
        {
            var cepSearch = new ViaCepClient();
            var result = new GeneralResult();

            try
            {
                result.data = cepSearch.Search(cep);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public GeneralResult Save(CotacaoDM cotacao)
        {
            var result = new GeneralResult();

            try
            {
                if (cotacao.Id > 0)
                    _context.CotacaoDMs.Update(cotacao);
                else
                {
                    cotacao.Status = Constants.Status.active;
                    _context.CotacaoDMs.Add(cotacao);
                }

                _context.SaveChanges();

                result.data = cotacao;
                result.AddMessage(Messages.Success.save);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public GeneralResult VerifyFields(CotacaoRequest request, CotacaoDM cotacao)
        {
            var result = new GeneralResult();

            try
            {

                if (!string.IsNullOrEmpty(request.cnpjComprador.Trim()))
                    cotacao.CNPJComprador = request.cnpjComprador;
                else
                    result.AddError(Messages.Errors.cnpjCrequired);

                if (!string.IsNullOrEmpty(request.cnpjFornecedor.Trim()))
                    cotacao.CNPJFornecedor = request.cnpjFornecedor;
                else
                    result.AddError(Messages.Errors.cnpjFRequired);

                if (request.numeroCotacao != null && request.numeroCotacao > 0)
                    cotacao.NumeroCotacao = (int)request.numeroCotacao;

                if (request.dataCotacao != null)
                    cotacao.DataCotacao = (DateOnly)request.dataCotacao;

                if (request.dataEntregaCotacao != null)
                    cotacao.DataEntregaCotacao = (DateOnly)request.dataEntregaCotacao;

                if (!string.IsNullOrEmpty(request.cep.Trim()))
                    cotacao.CEP = request.cep.Replace(".", "").Replace("-", "").Trim();
                else
                    result.AddError(Messages.Errors.CEPRequired);

                result.data = cotacao;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public GeneralResult CheckCEP(CotacaoDM cotacao)
        {
            var result = new GeneralResult();

            try
            {
                if (string.IsNullOrEmpty(cotacao.Logradouro.Trim()) || string.IsNullOrEmpty(cotacao.Complemento.Trim()) || string.IsNullOrEmpty(cotacao.Bairro.Trim()) || string.IsNullOrEmpty(cotacao.UF.Trim()))
                {
                    var address = (ViaCepResult)GetAddress(cotacao.CEP).data;

                    cotacao.Logradouro = address.Street;
                    cotacao.Complemento = address.Complement;
                    cotacao.Bairro = address.Neighborhood;
                    cotacao.UF = address.StateInitials;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public GeneralResult Save(CotacaoRequest request)
        {
            var result = new GeneralResult();
            CotacaoDM cotacao = null;
            var cotacaoItems = new CotacaoItem(_context);

            try
            {
                if (request.id != null && request.id > 0)
                    cotacao = Get((int)request.id);
                else
                    cotacao = new CotacaoDM();

                result = VerifyFields(request, cotacao);
                cotacao = (CotacaoDM)result.data;

                cotacao.Logradouro = request.logradouro;
                cotacao.Complemento = request.completemento;
                cotacao.Bairro = request.bairro;
                cotacao.UF = request.uf;

                result = CheckCEP(cotacao);
                cotacao = (CotacaoDM)result.data;

                cotacao.Observacao = request.observacao;

                if (!result.failure)
                    result = Save(cotacao);


                foreach (var item in request?.cotacaoItens)
                {
                    item.idCotacao = cotacao.Id;
                    result = cotacaoItems.Save(item);
                }

                if (!result.failure)
                    result.data = new
                    {
                        cotacao,
                        CotacaoItens = cotacaoItems.GetByCotacao(cotacao.Id)
                    };
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }
        public GeneralResult Delete(int id)
        {
            var result = new GeneralResult();
            CotacaoDM cotacao = null;

            try
            {
                cotacao = Get(id);

                if (cotacao != null)
                {
                    cotacao.Status = Constants.Status.inactive;
                    Save(cotacao);
                    result.AddMessage(Messages.Success.delete);
                }
                else
                    result.AddError(Messages.Errors.cotacaoNotFound);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }
    }


    public class CotacaoRequest
    {
        public int? id { get; set; }

        public string cnpjComprador { get; set; }

        public string cnpjFornecedor { get; set; }

        public int? numeroCotacao { get; set; }

        public DateOnly? dataCotacao { get; set; }

        public DateOnly? dataEntregaCotacao { get; set; }

        public string cep { get; set; }

        public string logradouro { get; set; }

        public string completemento { get; set; }

        public string bairro { get; set; }

        public string uf { get; set; }

        public string observacao { get; set; }

        public List<ItemRequest> cotacaoItens { get; set; }
    }

}
