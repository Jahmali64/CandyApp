using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public interface IPirateKingDbContext : IAsyncDisposable {
    DbSet<Address> Addresses { get; set; }

    DbSet<BuildVersion> BuildVersions { get; set; }

    DbSet<Customer> Customers { get; set; }

    DbSet<CustomerAddress> CustomerAddresses { get; set; }

    DbSet<ErrorLog> ErrorLogs { get; set; }

    DbSet<Fan> Fans { get; set; }

    DbSet<Product> Products { get; set; }

    DbSet<ProductCategory> ProductCategories { get; set; }

    DbSet<ProductDescription> ProductDescriptions { get; set; }

    DbSet<ProductModel> ProductModels { get; set; }

    DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

    DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    DbSet<VGetAllCategory> VGetAllCategories { get; set; }

    DbSet<VProductAndDescription> VProductAndDescriptions { get; set; }

    DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}

public partial class PirateKingDbContext : DbContext, IPirateKingDbContext {
}