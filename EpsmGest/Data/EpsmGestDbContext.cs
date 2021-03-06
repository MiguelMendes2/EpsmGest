using EPSMGest.Models;
using EPSMGest.Models.Purchase;
using EPSMGest.Models.Space;
using EPSMGest.Models.Stocks;
using EPSMGest.Models.Supplier;
using EPSMGest.Models.Vehicle;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EpsmGest.Data
{
    public class EpsmGestDbContext : IdentityDbContext<IdentityUser>
    {
        public EpsmGestDbContext(DbContextOptions<EpsmGestDbContext> options)
            : base(options)
        { }

        // --- Intervention ---

        // --- Purchase ---

        public DbSet<PurchaseModel> Purchase { get; set; }

        public DbSet<PurchaseItemModel> PurchaseItem { get; set; }

        // --- Space ---

        public DbSet<RequestSpaceModel> RequestSpace { get; set; }

        public DbSet<SpaceModel> Space { get; set; }

        // --- Stocks ---

        public DbSet<EquipmentModel> Equipment { get; set; }

        public DbSet<StockModel> Stock { get; set; }

        public DbSet<StockCategoryModel> StockCategory { get; set; }

        // --- Supplier ---

        public DbSet<SupplierEvaluationModel> SupplierEvaluation { get; set; }

        public DbSet<SupplierModel> Supplier { get; set; }

        // --- Vehicle ---

        public DbSet<RequestVehicleModel> RequestVehicle { get; set; }

        public DbSet<VehicleModel> Vehicle { get; set; }

        // --- Others ---

        public DbSet<DepartmentModel> Department { get; set; }

        public DbSet<InvoiceModel> Invoice { get; set; }

        public DbSet<RequisitionModel> Requisition { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PurchaseModel>();

            builder.Entity<PurchaseItemModel>();

            builder.Entity<DepartmentModel>();

            builder.Entity<InvoiceModel>();

            builder.Entity<RequisitionModel>();

            builder.Entity<SupplierModel>();

        }
    }
}