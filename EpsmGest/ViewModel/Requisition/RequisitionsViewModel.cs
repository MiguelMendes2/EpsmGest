namespace EpsmGest.ViewModel.Requisition
{
	public class RequisitionsViewModel
	{
        public string RequisicaoId { get; set; }

        public string Department { get; set; }

        public string Applicant { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public string? FilesName { get; set; }
    }
}
