netcore 2.0
NLog
AutoMapper
CodeFirst
***********************************************************************CodeFirst************************************************************************************
a): Entity

b): Startup:
            services.AddDbContext<MyContext>(options => { options.UseSqlServer(connectionString); });

c): DBContext:
	public DbSet<Product> Products { get; set; }

	base(options);
	//Database.EnsureCreated();
	Database.Migrate();

d): Fluet Api£∫
protected override void OnModelCreating£®ModelBuilder modelBuilder£©
{
	...
	modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
	...
	base.OnModelCreating(modelBuilder);
}

d1): IEntityTypeConfiguration<T>

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.ToTable("Products");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Price).HasColumnType("decimal(8,2)");
            //builder.Property(e => e.Description).HasMaxLength(200);
        }
    }

Add-Migration ProductInfoDbInitialMigration
Update-Database -verbose  °æDatabase.Migrate();°ø

Add-Migration AddDescriptionToProduct
Update-Database -verbose

APPSettings£∫connectionStrings≈‰÷√
***********************************************************************CodeFirst************************************************************************************	