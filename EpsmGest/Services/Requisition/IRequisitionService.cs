﻿using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Models;

namespace EPSMGest.Services.Requisition
{
    public interface IRequisitionService
    {
        public List<RequisitionsViewModel> GetRequisitions();

        public List<RequisitionsViewModel> GetUserRequisitions(string userName);

        public RequisitionsViewModel GetRequisition(string Id);

        public List<DropdownViewModel> GetRequisitionsIds();

        public Task CreateRequisition(CreateRequisitionViewModel model);

        public void EditRequisition(RequisitionModel model);

        public bool DeleteRequisition(string Id);
    }
}
