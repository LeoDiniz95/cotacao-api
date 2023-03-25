using cotacao_api.Data;
using cotacao_api.General;
using cotacao_api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cotacao_api.Repositories
{
    public class CotacaoItem
    {
        private DataContext _context { get; }
        public CotacaoItem(DataContext context)
        {
            _context = context;
        }

        public GeneralResult GetAll()
        {
            var result = new GeneralResult();

            try
            {
                result.data = _context.CotacaoItemDMs?.Where(x => x.Status == Constants.Status.active);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public CotacaoItemDM Get(int id) => _context.CotacaoItemDMs?.SingleOrDefault(x => x.Id == id && x.Status == Constants.Status.active);

        public List<CotacaoItemDM> GetByCotacao(int idCotacao) => _context.CotacaoItemDMs?.Where(x => x.IdCotacao == idCotacao && x.Status == Constants.Status.active).ToList();

        public GeneralResult Save(CotacaoItemDM item)
        {
            var result = new GeneralResult();

            try
            {
                if (item.Id > 0)
                    _context.CotacaoItemDMs.Update(item);
                else
                {
                    item.Status = Constants.Status.active;
                    _context.CotacaoItemDMs.Add(item);
                }

                _context.SaveChanges();

                result.data = item;
                result.AddMessage(Messages.Success.save);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public GeneralResult Save(ItemRequest request)
        {
            var result = new GeneralResult();
            CotacaoItemDM item = null;

            try
            {
                if (request.id != null && request.id > 0)
                    item = Get((int)request.id);
                else
                    item = new CotacaoItemDM();

                item.IdCotacao = request.idCotacao;

                if (!string.IsNullOrEmpty(request.descricao))
                    item.Descricao = request.descricao;
                else
                    result.AddMessage(Messages.Errors.DescricaoRequired);

                if (request.numeroItem > 0)
                    item.NumeroItem = request.numeroItem;
                else
                    result.AddMessage(Messages.Errors.numeroItemRequired);

                item.Preco = request.preco;

                if (request.quantidade > 0)
                    item.Quantidade = request.quantidade;
                else
                    result.AddMessage(Messages.Errors.QuantidadeRequired);

                item.Marca = request.marca;
                item.Unidade = request.unidade;

                if (!result.failure)
                    result = Save(item);
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
            CotacaoItemDM item = null;

            try
            {
                item = Get(id);

                if (item != null)
                {
                    item.Status = Constants.Status.inactive;
                    result = Save(item);
                    result.AddMessage(Messages.Success.delete);
                }
                else
                    result.AddError(Messages.Errors.itemNotFound);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

    }

    public class ItemRequest
    {
        public int? id { get; set; }

        public int idCotacao { get; set; }

        public string descricao { get; set; }

        public int numeroItem { get; set; }

        public decimal preco { get; set; }

        public int quantidade { get; set; }

        public string marca { get; set; }

        public string unidade { get; set; }
    }
}
