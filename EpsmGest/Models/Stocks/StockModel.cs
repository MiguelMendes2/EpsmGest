using EPSMGest.Models.Stocks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models.Stocks
{
	public class StockModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("StockCategory")]
		public int CategoryId { get; set; }
		public StockCategoryModel StockCategory { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }
	}
}
