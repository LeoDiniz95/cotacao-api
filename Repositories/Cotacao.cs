using cotacao_api.Data;
using cotacao_api.General;
using cotacao_api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

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

            try
            {
                result.data = _context.CotacaoDMs?.Where(x => x.Status == Constants.Status.active);
            }
            catch (Exception ex)
            {
                result.AddError(ex);
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
                result.AddError(ex);
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
                result.AddError(ex);
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

                if (!string.IsNullOrEmpty(request.cnpjComprador))
                    cotacao.CNPJComprador = request.cnpjComprador;
                else
                    result.AddError(Messages.Errors.cnpjCrequired);

                if (!string.IsNullOrEmpty(request.cnpjFornecedor))
                    cotacao.CNPJFornecedor = request.cnpjFornecedor;
                else
                    result.AddError(Messages.Errors.cnpjFRequired);

                if (request.numeroCotacao != null && request.numeroCotacao > 0)
                    cotacao.NumeroCotacao = (int)request.numeroCotacao;

                if (request.dataCotacao != null)
                    cotacao.DataCotacao = (DateOnly)request.dataCotacao;

                if (request.dataEntregaCotacao != null)
                    cotacao.DataEntregaCotacao = (DateOnly)request.dataEntregaCotacao;

                if (!string.IsNullOrEmpty(request.cep))
                    cotacao.CEP = request.cep;
                else
                    result.AddError(Messages.Errors.CEPRequired);

                if (!result.failure)
                {
                    cotacao.Logradouro = request.logradouro;
                    cotacao.Complemento = request.completemento;
                    cotacao.Bairro = request.bairro;
                    cotacao.UF = request.uf;

                    if (string.IsNullOrEmpty(cotacao.Logradouro) || string.IsNullOrEmpty(cotacao.Complemento) || string.IsNullOrEmpty(cotacao.Bairro) || string.IsNullOrEmpty(cotacao.UF))
                    {
                        var address = (ViaCepResult)GetAddress(cotacao.CEP).data;

                        if (string.IsNullOrEmpty(cotacao.Logradouro))
                            cotacao.Logradouro = address.Street;

                        if (string.IsNullOrEmpty(cotacao.Complemento))
                            cotacao.Complemento = address.Complement;

                        if (string.IsNullOrEmpty(cotacao.Bairro))
                            cotacao.Bairro = address.Neighborhood;

                        if (string.IsNullOrEmpty(cotacao.UF))
                            cotacao.UF = address.StateInitials;
                    }

                    cotacao.Observacao = request.observacao;

                    if (!result.failure)
                        result = Save(cotacao);

                    if (!result.failure)
                    {
                        foreach (var item in request?.cotacaoItens)
                        {
                            item.idCotacao = cotacao.Id;
                            result = cotacaoItems.Save(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex);
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
                    result = Save(cotacao);
                    result.AddMessage(Messages.Success.delete);
                }
                else
                    result.AddError(Messages.Errors.cotacaoNotFound);
            }
            catch (Exception ex)
            {
                result.AddError(ex);
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
