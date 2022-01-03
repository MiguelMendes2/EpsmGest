using EPSMGest.Models;
using EpsmGest.Data;
using EPSMGest.Services.Requisicao;
using EpsmGest.ViewModel;

namespace EPSMGest.Services.Requisicao
{
    public class RequisicaoService : IRequisicaoService
    {
        public readonly EpsmGestDbContext AppDb;

        public RequisicaoService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<RequisicoesModel> GetRequesicoes()
        {
            return AppDb.Requisicoes.Join(AppDb.Departamento,
                req => Convert.ToInt32(req.DepartamentoId),
                dep => dep.DepartamentoId,
                (req, dep) => new RequisicoesModel
                {
                    RequisicaoId = req.RequisicaoId,
                    Requerente = req.Requerente,
                    DepartamentoId = dep.Nome,
                    Descricao = req.Descricao,
                    date = req.date,
                }).ToList();
        }

        public List<RequisicoesModel> GetUserRequesicoes(string userName)
        {
            return AppDb.Requisicoes.Where(x => x.Requerente == userName).ToList();
        }

        public RequisicoesModel GetRequesicao(string Id)
        {
            return AppDb.Requisicoes.FirstOrDefault(x => x.RequisicaoId == Id.ToString());
        }

        public List<DropdownViewModel> GetRequesicaoIds()
        {
            return AppDb.Requisicoes.Select(x => new DropdownViewModel { Id = x.RequisicaoId, Name = x.RequisicaoId }).ToList();
        }

        public void CreateRequesicao(RequisicoesModel model)
        {
            var lastReq = AppDb.Requisicoes.OrderByDescending(x => x.RequisicaoId).FirstOrDefault();
            if (lastReq != null)
            {
                var reqId = lastReq.RequisicaoId.Split('_');
                int nextint = Convert.ToInt32(reqId[2]) + 1;
                if (DateTime.Now.ToString("yyyy") == reqId[0])
                {
                    model.RequisicaoId = "Req_" + reqId[1] + "_" + nextint.ToString();
                }
                else
                {
                    model.RequisicaoId = "Req_" + DateTime.Now.ToString("yyyy") + "_" + nextint.ToString();
                }
            }
            else
            {
                model.RequisicaoId = "Req_" + DateTime.Now.ToString("yyyy") + "_1";
            }
            AppDb.Requisicoes.Add(model);
            AppDb.SaveChanges();
        }

        public void EditRequesicao(RequisicoesModel model)
        {
            AppDb.Requisicoes.Update(model);
            AppDb.SaveChanges();
        }

        public bool DeleteRequesicao(string Id)
        {
            var requiscao = AppDb.Requisicoes.FirstOrDefault(x => x.RequisicaoId == Id);
            if (requiscao != null)
            {
                var compra = AppDb.Compra.FirstOrDefault(x => x.IdRequisicao == Id);
                if (compra == null)
                {
                    AppDb.Requisicoes.Remove(requiscao);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }


    }
}
