using EpsmGest.Data;
using EPSMGest.Models.Purchase;
using EPSMGest.Models.Supplier;
using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Purchase;
using EpsmGest.ViewModel.Requisition;
using Microsoft.EntityFrameworkCore;
using EPSMGest.Models;

namespace EPSMGest.Services.Purchase
{
    public class PurchaseService : IPurchaseService
    {
        public readonly EpsmGestDbContext AppDb;

        public PurchaseService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<PurchaseModel> GetPurchases()
        {
            return AppDb.Purchase.Include(x => x.Requisition).ToList();
        }

        public PurchaseModel? GetPurchase(int id)
        {
            return AppDb.Purchase.Include(x => x.Requisition).FirstOrDefault(x => x.Id == id);
        }

        public List<DropdownViewModel> GetPurchasesIds()
        {
            return AppDb.Purchase.Include(x => x.Requisition).Select(x => new DropdownViewModel { Id = x.Id.ToString(), Name = x.PurchaseId }).ToList();
        }

        public async Task CreatePurchase(CreateReqPurchaseViewModel model)
        {
            PurchaseModel compra = new()
			{
                PurchaseId = PurchaseId(),
                Stage = 0,
                Date = DateTime.Now,
                RequisitionId = model.Requisition.RequisicaoId
            };
            AppDb.Purchase.Add(compra);
            AppDb.SaveChanges();

            for (int i = 0; i < model.PurchaseItem.Count; i++)
			{
                model.PurchaseItem[i].PurchaseId = compra.Id;
                model.PurchaseItem[i].Purchase = compra;
            }
            compra.PurchaseItem = (ICollection<PurchaseItemModel>)model.PurchaseItem;

            AppDb.PurchaseItem.AddRange(compra.PurchaseItem);
            AppDb.Purchase.Update(compra);
            await AppDb.SaveChangesAsync();
        }

        public void EditPurchase(PurchaseModel model)
        {
            AppDb.Purchase.Update(model);
            AppDb.SaveChanges();
        }

        public bool DeletePurchase(int Id)
        {
            var model = AppDb.Purchase.Include(x => x.Requisition).FirstOrDefault(x => x.Id == Id);
            if (model != null)
            {
                var dependentrows = AppDb.Purchase.FirstOrDefault(x => x.Id == Id);
                if (dependentrows == null)
                {
                    var requesicao = AppDb.Requisition.FirstOrDefault(x => x.RequisicaoId == model.RequisitionId);
                    var Items = AppDb.PurchaseItem.Where(x => x.PurchaseId == model.Id);

                    if (requesicao != null)
                        AppDb.Requisition.Remove(requesicao);
                    if(Items != null)
                        AppDb.PurchaseItem.RemoveRange(Items);
                    model.Requisition.Delivered = 1;
                    AppDb.Purchase.Remove(model);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        // --- Necessidade ---

        public NecessidadeViewModel? GetNecessidade(int id)
		{
            var compra = AppDb.Purchase.Include(x => x.PurchaseItem).Include(x => x.Requisition).FirstOrDefault(x => x.Id == id);
            if (compra == null)
                return null;
            return new NecessidadeViewModel()
            {
                Id = id,
                PurchaseId = compra.PurchaseId,
                RequisitionId = compra.RequisitionId,
                Stage = compra.Stage,
                Applicant = AppDb.Requisition.First(x => x.RequisicaoId == compra.RequisitionId).Applicant,
                Department = AppDb.Department.First(x => x.DepartamentId == compra.Requisition.DepartamentId).Name,
                DepartamentId = compra.Requisition.DepartamentId,
                Date = compra.Requisition.Date,
                Description = compra.Requisition.Description,
                PurchaseItem = compra.PurchaseItem.Select(x => new SimplePurchaseItemViewModel { Id = x.Id, Quantity = x.Quantity, Name = x.Item }).ToList()
            };
        }

        public bool EditNecessidade(NecessidadeViewModel model)
		{
            var compra = AppDb.Purchase.Include(x => x.Requisition).FirstOrDefault(x => x.Id == model.Id);
            if(compra != null)
			{
                compra.Requisition.DepartamentId = model.DepartamentId;
                compra.Requisition.Description = model.Description;
                if(model.PurchaseItem != null)
				{
                    List<PurchaseItemModel> PurchaseItems = new();
                    for (int i = 0; i < model.PurchaseItem.Count; i++)
					{
                        if(model.PurchaseItem[i].Id != 0)
						{
                            var PurchaseItem = AppDb.PurchaseItem.FirstOrDefault(x => x.Id == model.PurchaseItem[i].Id);
                            if(PurchaseItem != null)
							{
                                PurchaseItem.Item = model.PurchaseItem[i].Name;
                                PurchaseItem.Quantity = model.PurchaseItem[i].Quantity;
                                AppDb.Update(PurchaseItem);
                                break;
                            }
						}
                        AppDb.PurchaseItem.Add(new PurchaseItemModel { Item = model.PurchaseItem[i].Name, Quantity = model.PurchaseItem[i].Quantity });
					}
                    
                }
                AppDb.SaveChanges();
                return true;
            }
            return false;
            
		}

        // --- Consulta Mercado ---

        public ConsultaMercadoViewModel? GetConsultaMercado(int id)
		{
            return AppDb.Purchase.Include(x => x.PurchaseItem).ThenInclude(x => x.Supplier).Select(x => new ConsultaMercadoViewModel
            {
                Id = x.Id,
                Description = x.Requisition.Description,
                Stage = x.Stage,
                TotalPrice = x.TotalPrice,
                PurchaseItem = x.PurchaseItem.ToList()
            }).FirstOrDefault(x => x.Id == id);
        }

        public bool EditConsultaMercado(ConsultaMercadoViewModel model)
		{
            var compra = AppDb.Purchase.Include(x=> x.PurchaseItem).FirstOrDefault(x => x.Id == model.Id);
			if (compra != null && compra.Stage == 0)
			{
                decimal total = 0;
                var updatedRows = new List<PurchaseItemModel>();
                for (int i = 0; i < model.PurchaseItem.Count; i++)
				{
                    
                    var item = compra.PurchaseItem.FirstOrDefault(x => x.Id == model.PurchaseItem[i].Id);
                    if(item != null)
					{
                        var supplier = AppDb.Supplier.FirstOrDefault(x => x.Name == model.PurchaseItem[i].Supplier.Name);
                        if (supplier == null)
						{
                            supplier = new SupplierModel{ Name = model.PurchaseItem[i].Supplier.Name };
                            AppDb.Supplier.Add(supplier);
                            AppDb.SaveChanges();
                        }
                        item.Id = model.PurchaseItem[i].Id;
                        item.SupplierId = supplier.SupplierId;
                        item.Price = model.PurchaseItem[i].Price;
                        item.Quantity = model.PurchaseItem[i].Quantity; 
                        
                        updatedRows.Add(item);
                        total += item.Price * item.Quantity;
                    }
				}
                AppDb.PurchaseItem.UpdateRange(updatedRows);
                compra.TotalPrice = total;
                AppDb.SaveChanges();
                return true;
            }
            return false;
		}

        public bool CloseConsultaMercado(int id)
		{
            var compra = AppDb.Purchase.Include(x => x.PurchaseItem).FirstOrDefault(x => x.Id == id);
            if(compra != null)
			{
                bool flag = true;
                foreach (var Item in compra.PurchaseItem)
                {
                    if(Item.SupplierId == null && Item.Price == 0)
					{
                        flag = false;
                        break;
                    }  
                }
				if (flag){
                    compra.Stage = 1;
                    AppDb.SaveChanges();
                    return true;
				}
            }
            return false;
		}

        // --- Parecer 1 ---

        public bool SetParecer1(ConsultaMercadoViewModel model)
		{
            var compra = AppDb.Purchase.Include(x => x.PurchaseItem).FirstOrDefault(x => x.Id == model.Id);
            if (compra == null || compra.Stage != 1)
                return false;
            List<PurchaseItemModel> updatedItems = new();
            for (int i = 0; i < model.PurchaseItem.Count; i++)
            {
                var item = compra.PurchaseItem.FirstOrDefault(x => x.Id == model.PurchaseItem[i].Id);
                if (item != null)
                {
                    if (item.Aprovation1 != model.PurchaseItem[i].Aprovation1)
                    {
                        item.Aprovation1 = model.PurchaseItem[i].Aprovation1;
                        updatedItems.Add(item);
                    }
                }
            }
            AppDb.UpdateRange(updatedItems);
            AppDb.SaveChanges();
            return true;
		}

        public bool CloseParecer1(int id)
        {
            var compra = AppDb.Purchase.Include(x => x.PurchaseItem).FirstOrDefault(x => x.Id == id);
            if (compra == null || compra.Stage != 1)
                return false;
            if (compra.TotalPrice < 250)
			{
                compra.Stage = 3;
                foreach (var item in compra.PurchaseItem)
                    item.Aprovation2 = true;                  
            }                
            else
                compra.Stage = 2;
            AppDb.SaveChanges();
            return true;
        }

        // --- Parecer 2 ---

        public ConsultaMercadoViewModel? GetParecer2(int id)
        {
            return AppDb.Purchase.Include(x => x.PurchaseItem).ThenInclude(x => x.Supplier).Select(x => new ConsultaMercadoViewModel
            {
                Id = x.Id,
                Description = x.Requisition.Description,
                Stage = x.Stage,
                TotalPrice = x.TotalPrice,
                PurchaseItem = x.PurchaseItem.Where(x => x.Aprovation1 == true).ToList()
            }).FirstOrDefault(x => x.Id == id);
        }


        public bool SetParecer2(ConsultaMercadoViewModel model)
		{
            var compra = AppDb.Purchase.Include(x => x.PurchaseItem).FirstOrDefault(x => x.Id == model.Id);
            if (compra == null || compra.TotalPrice < 250 || compra.Stage != 2)
                return false;
            List<PurchaseItemModel> updatedItems = new();
            for (int i = 0; i < model.PurchaseItem.Count; i++)
            {
                var item = compra.PurchaseItem.FirstOrDefault(x => x.Id == model.PurchaseItem[i].Id);
                if (item != null)
                {
                    if (item.Aprovation2 != model.PurchaseItem[i].Aprovation2)
                    {
                        item.Aprovation2 = model.PurchaseItem[i].Aprovation2;
                        updatedItems.Add(item);
                    }
                }
            }
            AppDb.UpdateRange(updatedItems);
            AppDb.SaveChanges();
            return true;
        }

        public bool CloseParecer2(int id)
        {
            var compra = AppDb.Purchase.FirstOrDefault(x => x.Id == id);
            if (compra == null || compra.Stage != 2)
                return false;            
            compra.Stage = 3;
            AppDb.SaveChanges();
            return true;
        }

        // --- Evaluation ---

        public FornecedorEvaluationViewModel? GetFornecedorEvaluation(int id)
		{
            var compra = AppDb.Purchase.Include(x => x.Requisition).Include(x => x.PurchaseItem).ThenInclude(x => x.Supplier).FirstOrDefault(x => x.Id == id);
            if (compra == null)
                return null;
            FornecedorEvaluationViewModel model = new()
            {
                Id = id,
                Description = compra.Requisition.Description,
                Stage = compra.Stage,
                TotalPrice = compra.TotalPrice,
                Evaluations = new List<Evaluation>()               
            };
            var evaluations = AppDb.SupplierEvaluation.Where(x => x.PurchaseId == id);
            var SupplierTotals = compra.PurchaseItem.Where(x => x.Aprovation1 == true && x.Aprovation2 == true).GroupBy(x => x.SupplierId).Select(g => new { SupplierId = g.Key, SupplierTotal = g.Sum(x => x.Price) }).ToList();
            foreach(var Item in SupplierTotals)
			{
                var supplier = compra.PurchaseItem.FirstOrDefault(x => x.SupplierId == Item.SupplierId).Supplier;
                var evaluation = evaluations.FirstOrDefault(x => x.SupplierId == Item.SupplierId);
                var invoice = AppDb.Invoice.FirstOrDefault(x => x.SupplierId == Item.SupplierId && x.IdPurchase == compra.Id);
                if(supplier != null)
				{
                    if (evaluation != null)
                        model.Evaluations.Add(new Evaluation
                        {
                            EvalutionId = evaluation.Id,
                            SupplierId = evaluation.SupplierId,
                            SupplierName = supplier.Name,
                            Param0 = evaluation.Param0,
                            Param1 = evaluation.Param1,
                            Param2 = evaluation.Param2,
                            Param3 = evaluation.Param3,
                        });
                    else
                        model.Evaluations.Add(new Evaluation
                        {
                            SupplierId = supplier.SupplierId,
                            SupplierName = supplier.Name,
                        });
                    if (invoice == null)
                        model.Evaluations.FirstOrDefault(x => x.SupplierId == supplier.SupplierId).Invoice = new InvoiceModel{
                                IdPurchase = compra.Id,
                                SupplierId = Item.SupplierId,
                                TotalPrice = Item.SupplierTotal
                            };
                }              
            }
            return model;
		}

        public bool SetFornecedorEvaluation(FornecedorEvaluationViewModel model)
		{
            var compra = AppDb.Purchase.Include(x => x.PurchaseItem).ThenInclude(x => x.SupplierId).FirstOrDefault(x => x.Id == model.Id);
            if (compra == null)
                return false;
            List<SupplierEvaluationModel> fornecedoresEval = new();
            foreach (var evaluation in model.Evaluations)
			{
                var fornecedor = AppDb.Supplier.FirstOrDefault(x => x.SupplierId == evaluation.SupplierId);
                if (evaluation.EvalutionId != 0)
				{
                    
                    var fornecedorEvaluation = AppDb.SupplierEvaluation.FirstOrDefault(x => x.Id == evaluation.EvalutionId);
                    if(fornecedorEvaluation != null)
					{
                        fornecedorEvaluation.Param0 = evaluation.Param0;
                        fornecedorEvaluation.Param1 = evaluation.Param1;
                        fornecedorEvaluation.Param2 = evaluation.Param2;
                        fornecedorEvaluation.Param3 = evaluation.Param3;
                        AppDb.SaveChanges();
					}
					else
                        fornecedoresEval.Add(new SupplierEvaluationModel
                        {
                            PurchaseId = compra.Id,
                            SupplierId = evaluation.SupplierId,
                            Param0 = evaluation.Param0,
                            Param1 = evaluation.Param1,
                            Param2 = evaluation.Param2,
                            Param3 = evaluation.Param3
                        });
                }
				else 
                    fornecedoresEval.Add(new SupplierEvaluationModel
                    {
                        PurchaseId = compra.Id,
                        SupplierId = evaluation.SupplierId,
                        Param0 = evaluation.Param0,
                        Param1 = evaluation.Param1,
                        Param2 = evaluation.Param2,
                        Param3 = evaluation.Param3
                    });
                if(evaluation.Invoice.InvoiceNumber != 0)
				{
                    evaluation.Invoice.Date = DateTime.Now;
                    AppDb.Invoice.Add(evaluation.Invoice);
				}
			}
            AppDb.SupplierEvaluation.AddRange(fornecedoresEval);
            AppDb.SaveChanges();
            return true;
        }

        // Create New Id for Purchase
        public string PurchaseId()
		{
            string Id;
            var lastCompra = AppDb.Purchase.OrderByDescending(x => x.PurchaseId).FirstOrDefault();
            if (lastCompra != null)
            {
                var reqId = lastCompra.PurchaseId.Split('_');
                int nextint = Convert.ToInt32(reqId[2]) + 1;
                if (DateTime.Now.ToString("yyyy") == reqId[1])
                {
                    Id = "Comp_" + reqId[1] + "_" + nextint.ToString();
                }
                else
                {
                    Id = "Comp_" + DateTime.Now.ToString("yyyy") + "_" + nextint.ToString();
                }
            }
            else
            {
                Id = "Comp_" + DateTime.Now.ToString("yyyy") + "_1";
            }
            return Id;
		}
    }
}
