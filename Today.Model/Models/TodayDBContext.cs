using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Today.Model.Models
{
    public partial class TodayDBContext : DbContext
    {
        public TodayDBContext()
        {
        }

        public TodayDBContext(DbContextOptions<TodayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutProgram> AboutPrograms { get; set; }
        public virtual DbSet<AboutProgramOption> AboutProgramOptions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityRaider> CityRaiders { get; set; }
        public virtual DbSet<Collect> Collects { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LoginWay> LoginWays { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MinorCategory> MinorCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<PartnerType> PartnerTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PricingItem> PricingItems { get; set; }
        public virtual DbSet<PrimaryCategory> PrimaryCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramDatePicker> ProgramDatePickers { get; set; }
        public virtual DbSet<ProgramInclude> ProgramIncludes { get; set; }
        public virtual DbSet<ProgramSpecification> ProgramSpecifications { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<ShoppinCart> ShoppinCarts { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=TodayDB;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AboutProgram>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AboutProgram");

                entity.Property(e => e.AboutProgramOptionsId).HasColumnName("AboutProgramOptionsID");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.HasOne(d => d.AboutProgramOptions)
                    .WithMany()
                    .HasForeignKey(d => d.AboutProgramOptionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AboutProgram_AboutProgramOptions");

                entity.HasOne(d => d.Program)
                    .WithMany()
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AboutProgram_Program");
            });

            modelBuilder.Entity<AboutProgramOption>(entity =>
            {
                entity.HasKey(e => e.AboutProgramOptionsId);

                entity.Property(e => e.AboutProgramOptionsId)
                    .ValueGeneratedNever()
                    .HasColumnName("AboutProgramOptionsID");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IconClass).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Category");

                entity.Property(e => e.MinorCategoryId).HasColumnName("MinorCategoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.MinorCategory)
                    .WithMany()
                    .HasForeignKey(d => d.MinorCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_MinorCategory");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Product");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId)
                    .ValueGeneratedNever()
                    .HasColumnName("CityID");

                entity.Property(e => e.CityImg)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.CityIntrod).IsRequired();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CityRaider>(entity =>
            {
                entity.HasKey(e => e.RaidersId);

                entity.Property(e => e.RaidersId)
                    .ValueGeneratedNever()
                    .HasColumnName("RaidersID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.Video).IsRequired();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityRaiders)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityRaiders_City");
            });

            modelBuilder.Entity<Collect>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Collect");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collect_Member");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collect_Product");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("CommentID");

                entity.Property(e => e.CommentDate).HasColumnType("date");

                entity.Property(e => e.CommentText).IsRequired();

                entity.Property(e => e.CommentTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");

                entity.Property(e => e.PartnerTypeId).HasColumnName("PartnerTypeID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Member");

                entity.HasOne(d => d.OrderDetails)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.OrderDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_OrderDetails");

                entity.HasOne(d => d.PartnerType)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PartnerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_PartnerType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Product");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.CouponId)
                    .ValueGeneratedNever()
                    .HasColumnName("CouponID");

                entity.Property(e => e.Context).IsRequired();

                entity.Property(e => e.CouponDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CouponStatus)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .HasColumnName("longitude");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Product");
            });

            modelBuilder.Entity<LoginWay>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LoginWay");

                entity.Property(e => e.LongWayName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UniqueID");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginWay_Member");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.IdentityCard).HasMaxLength(10);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_City");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_Member_Coupon");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK_Member_Message");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("MessageID");

                entity.Property(e => e.MessageContext).IsRequired();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Order");
            });

            modelBuilder.Entity<MinorCategory>(entity =>
            {
                entity.ToTable("MinorCategory");

                entity.Property(e => e.MinorCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("MinorCategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrimaryCategoryId).HasColumnName("PrimaryCategoryID");

                entity.HasOne(d => d.PrimaryCategory)
                    .WithMany(p => p.MinorCategories)
                    .HasForeignKey(d => d.PrimaryCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MinorCategory_PrimaryCategory");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Payment");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId);

                entity.Property(e => e.OrderDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailsID");

                entity.Property(e => e.DetailJson)
                    .IsRequired()
                    .HasColumnName("DetailJSON");

                entity.Property(e => e.LeaseTime).HasColumnType("date");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TicketsId).HasColumnName("TicketsID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Product");

                entity.HasOne(d => d.Tickets)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.TicketsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Tickets");
            });

            modelBuilder.Entity<PartnerType>(entity =>
            {
                entity.ToTable("PartnerType");

                entity.Property(e => e.PartnerTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("PartnerTypeID");

                entity.Property(e => e.PartnerType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PartnerType");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId)
                    .ValueGeneratedNever()
                    .HasColumnName("PaymentID");

                entity.Property(e => e.PaymentWay)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PricingItem>(entity =>
            {
                entity.Property(e => e.PricingItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("PricingItemID");

                entity.Property(e => e.Itemtext)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OriginalUnitPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SpecificationId).HasColumnName("SpecificationID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UnitText)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.PricingItems)
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PricingItems_ProgramSpecification");
            });

            modelBuilder.Entity<PrimaryCategory>(entity =>
            {
                entity.ToTable("PrimaryCategory");

                entity.Property(e => e.PrimaryCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("PrimaryCategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitsOnOrder)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_City");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("ProductStatus");

                entity.Property(e => e.ProductStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductStatusID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductStatuses)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductStatus_Product");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.ProgramId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProgramID");

                entity.Property(e => e.Context).IsRequired();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Product");
            });

            modelBuilder.Entity<ProgramDatePicker>(entity =>
            {
                entity.HasKey(e => e.ProgramDateId);

                entity.ToTable("ProgramDatePicker");

                entity.Property(e => e.ProgramDateId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProgramDateID");

                entity.Property(e => e.DatimePickerConfigurationJson)
                    .IsRequired()
                    .HasColumnName("DatimePickerConfigurationJSON");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramDatePickers)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramDatePicker_Program");
            });

            modelBuilder.Entity<ProgramInclude>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProgramInclude");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.ProgramIncludeId).HasColumnName("ProgramIncludeID");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Program)
                    .WithMany()
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramInclude_Program");
            });

            modelBuilder.Entity<ProgramSpecification>(entity =>
            {
                entity.HasKey(e => e.SpecificationId);

                entity.ToTable("ProgramSpecification");

                entity.Property(e => e.SpecificationId)
                    .ValueGeneratedNever()
                    .HasColumnName("SpecificationID");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.SpecificationJson)
                    .IsRequired()
                    .HasColumnName("SpecificationJSON");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramSpecifications)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramSpecification_Program");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("Reply");

                entity.Property(e => e.ReplyId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReplyID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ReplayText).IsRequired();

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reply_Message");
            });

            modelBuilder.Entity<ShoppinCart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ShoppinCart");

                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.SpecificationId).HasColumnName("SpecificationID");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppinCart_Member");

                entity.HasOne(d => d.Specification)
                    .WithMany()
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppinCart_ProgramSpecification");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription");

                entity.Property(e => e.SubscriptionId)
                    .ValueGeneratedNever()
                    .HasColumnName("SubscriptionID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId)
                    .ValueGeneratedNever()
                    .HasColumnName("SupplierID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactTitle).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_City");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.TagId)
                    .ValueGeneratedNever()
                    .HasColumnName("TagID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_Product");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketsId);

                entity.Property(e => e.TicketsId)
                    .ValueGeneratedNever()
                    .HasColumnName("TicketsID");

                entity.Property(e => e.TicketsQrcode)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("TicketsQRcode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
