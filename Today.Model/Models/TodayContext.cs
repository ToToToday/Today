using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Today.Model.Models
{
    public partial class TodayContext : DbContext
    {
        public TodayContext()
        {
        }

        public TodayContext(DbContextOptions<TodayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityRaider> CityRaiders { get; set; }
        public virtual DbSet<Collect> Collects { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponDetail> CouponDetails { get; set; }
        public virtual DbSet<EventLocation> EventLocations { get; set; }
        public virtual DbSet<HowUse> HowUses { get; set; }
        public virtual DbSet<HowUseDetail> HowUseDetails { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<LocationDetail> LocationDetails { get; set; }
        public virtual DbSet<LoginWay> LoginWays { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberInfoId> MemberInfoIds { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MinorCategory> MinorCategories { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OffersDetail> OffersDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetai> OrderDetais { get; set; }
        public virtual DbSet<PamerType> PamerTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PrimaryCategory> PrimaryCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PromotionWay> PromotionWays { get; set; }
        public virtual DbSet<StoreMessage> StoreMessages { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagDetail> TagDetails { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TodayMessage> TodayMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=Today;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("Ad");

                entity.Property(e => e.AdId)
                    .ValueGeneratedNever()
                    .HasColumnName("AdID");

                entity.Property(e => e.AdName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Peroid).HasColumnType("date");
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.ToTable("CarModel");

                entity.Property(e => e.CarModelId)
                    .ValueGeneratedNever()
                    .HasColumnName("CarModelID");

                entity.Property(e => e.CarModel1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CarModel");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId)
                    .ValueGeneratedNever()
                    .HasColumnName("CityID");

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

                entity.Property(e => e.CityId).HasColumnName("CItyID");

                entity.Property(e => e.Subtitle).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Video).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityRaiders)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityRaiders_City");
            });

            modelBuilder.Entity<Collect>(entity =>
            {
                entity.ToTable("Collect");

                entity.Property(e => e.CollectId)
                    .ValueGeneratedNever()
                    .HasColumnName("CollectID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Collects)
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

                entity.Property(e => e.CommentTitle).HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ParnerTypeId).HasColumnName("ParnerTypeID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_MemberInfoID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Order");

                entity.HasOne(d => d.ParnerType)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ParnerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_PamerType");

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

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CouponDetail>(entity =>
            {
                entity.HasKey(e => e.CouponDetailsId);

                entity.Property(e => e.CouponDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("CouponDetailsID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponDetails)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponDetails_Coupon");
            });

            modelBuilder.Entity<EventLocation>(entity =>
            {
                entity.ToTable("EventLocation");

                entity.Property(e => e.EventLocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("EventLocationID");

                entity.Property(e => e.LocationDetailsId).HasColumnName("LocationDetailsID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.LocationDetails)
                    .WithMany(p => p.EventLocations)
                    .HasForeignKey(d => d.LocationDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventLocation_LocationDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.EventLocations)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventLocation_Product");
            });

            modelBuilder.Entity<HowUse>(entity =>
            {
                entity.ToTable("HowUse");

                entity.Property(e => e.HowUseId)
                    .ValueGeneratedNever()
                    .HasColumnName("HowUseID");

                entity.Property(e => e.HowUseDetailsId).HasColumnName("HowUseDetailsID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.HowUseDetails)
                    .WithMany(p => p.HowUses)
                    .HasForeignKey(d => d.HowUseDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HowUse_HowUseDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HowUses)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HowUse_Product");
            });

            modelBuilder.Entity<HowUseDetail>(entity =>
            {
                entity.HasKey(e => e.HowUseDetailsId);

                entity.Property(e => e.HowUseDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("HowUseDetailsID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("InvoiceID");

                entity.Property(e => e.InvoiceWay)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LocationDetail>(entity =>
            {
                entity.HasKey(e => e.LocationDetailsId)
                    .HasName("PK_EventLocation");

                entity.Property(e => e.LocationDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationDetailsID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoginWay>(entity =>
            {
                entity.ToTable("LoginWay");

                entity.Property(e => e.LoginWayId)
                    .ValueGeneratedNever()
                    .HasColumnName("LoginWayID");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Member");

                entity.Property(e => e.CollectId).HasColumnName("CollectID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.Collect)
                    .WithMany()
                    .HasForeignKey(d => d.CollectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Collect");

                entity.HasOne(d => d.Coupon)
                    .WithMany()
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Coupon");

                entity.HasOne(d => d.MemberNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_MemberInfoID");

                entity.HasOne(d => d.Message)
                    .WithMany()
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Message");
            });

            modelBuilder.Entity<MemberInfoId>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("MemberInfoID");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.Brithday).HasColumnType("date");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LoginWayId).HasColumnName("LoginWayID");

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pchone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.MemberInfoIds)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_MemberInfoID_City");

                entity.HasOne(d => d.LoginWay)
                    .WithMany(p => p.MemberInfoIds)
                    .HasForeignKey(d => d.LoginWayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberInfoID_LoginWay");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("MessageID");

                entity.Property(e => e.StoreMessageId).HasColumnName("StoreMessageID");

                entity.Property(e => e.TodayMessageId).HasColumnName("TodayMessageID");

                entity.HasOne(d => d.StoreMessage)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.StoreMessageId)
                    .HasConstraintName("FK_Message_StoreMessage");

                entity.HasOne(d => d.TodayMessage)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.TodayMessageId)
                    .HasConstraintName("FK_Message_TodayMessage");
            });

            modelBuilder.Entity<MinorCategory>(entity =>
            {
                entity.ToTable("MinorCategory");

                entity.Property(e => e.MinorCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("MinorCategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PrimaryCategoryId).HasColumnName("PrimaryCategoryID");

                entity.HasOne(d => d.PrimaryCategory)
                    .WithMany(p => p.MinorCategories)
                    .HasForeignKey(d => d.PrimaryCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MinorCategory_PrimaryCategory");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(e => e.OffersId);

                entity.Property(e => e.OffersId)
                    .ValueGeneratedNever()
                    .HasColumnName("OffersID");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.PromotionWayId).HasColumnName("PromotionWayID");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK_Offers_Ad");

                entity.HasOne(d => d.PromotionWay)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.PromotionWayId)
                    .HasConstraintName("FK_Offers_PromotionWay");
            });

            modelBuilder.Entity<OffersDetail>(entity =>
            {
                entity.HasKey(e => e.OffersDetailsId);

                entity.Property(e => e.OffersDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("OffersDetailsID");

                entity.Property(e => e.OffersId).HasColumnName("OffersID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Offers)
                    .WithMany(p => p.OffersDetails)
                    .HasForeignKey(d => d.OffersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffersDetails_Offers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OffersDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffersDetails_Product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.LeaseTime).HasColumnType("date");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.SumPrice).HasColumnType("money");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_Order_Invoice");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_MemberInfoID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Order_Payment");
            });

            modelBuilder.Entity<OrderDetai>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId);

                entity.Property(e => e.OrderDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailsID");

                entity.Property(e => e.CarModelId).HasColumnName("CarModelID");

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ShowTime).HasColumnType("datetime");

                entity.Property(e => e.TicketsId).HasColumnName("TicketsID");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK_OrderDetais_CarModel");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetais_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetais_Product");

                entity.HasOne(d => d.Tickets)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.TicketsId)
                    .HasConstraintName("FK_OrderDetais_Tickets");
            });

            modelBuilder.Entity<PamerType>(entity =>
            {
                entity.ToTable("PamerType");

                entity.Property(e => e.PamerTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("PamerTypeID");

                entity.Property(e => e.PamerType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PamerType");
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

                entity.Property(e => e.MinorCategoryId).HasColumnName("MinorCategoryID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_City");

                entity.HasOne(d => d.MinorCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MinorCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_MinorCategory");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<PromotionWay>(entity =>
            {
                entity.ToTable("PromotionWay");

                entity.Property(e => e.PromotionWayId)
                    .ValueGeneratedNever()
                    .HasColumnName("PromotionWayID");

                entity.Property(e => e.PromotionWayName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StoreMessage>(entity =>
            {
                entity.ToTable("StoreMessage");

                entity.Property(e => e.StoreMessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("StoreMessageID");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId)
                    .ValueGeneratedNever()
                    .HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactName).HasMaxLength(50);

                entity.Property(e => e.ContactTitle).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

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

                entity.Property(e => e.TagDetailsId).HasColumnName("TagDetailsID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_Product");

                entity.HasOne(d => d.TagDetails)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.TagDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_TagDetails");
            });

            modelBuilder.Entity<TagDetail>(entity =>
            {
                entity.HasKey(e => e.TagDetailsId)
                    .HasName("PK_Rule");

                entity.Property(e => e.TagDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("TagDetailsID");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId)
                    .ValueGeneratedNever()
                    .HasColumnName("TicketID");

                entity.Property(e => e.TicketsName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TodayMessage>(entity =>
            {
                entity.ToTable("TodayMessage");

                entity.Property(e => e.TodayMessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("TodayMessageID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
