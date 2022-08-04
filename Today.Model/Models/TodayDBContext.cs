using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Today.Model.Models
{
    public partial class TodayDbContext : DbContext
    {
        public TodayDbContext()
        {
        }

        public TodayDbContext(DbContextOptions<TodayDbContext> options)
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
        public virtual DbSet<CouponManage> CouponManages { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LoginWay> LoginWays { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramCantUseDate> ProgramCantUseDates { get; set; }
        public virtual DbSet<ProgramInclude> ProgramIncludes { get; set; }
        public virtual DbSet<ProgramSpecification> ProgramSpecifications { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=todaysqlserver.database.windows.net;Initial Catalog=TodayDb;User ID=bs;Password=P@ssword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_BIN");

            modelBuilder.Entity<AboutProgram>(entity =>
            {
                entity.ToTable("AboutProgram");

                entity.HasIndex(e => e.AboutProgramOptionsId, "IX_AboutProgram_AboutProgramOptionsId");

                entity.HasIndex(e => e.ProgramId, "IX_AboutProgram_ProgramId");

                entity.HasOne(d => d.AboutProgramOptions)
                    .WithMany(p => p.AboutPrograms)
                    .HasForeignKey(d => d.AboutProgramOptionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AboutProgram_AboutProgramOptions");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.AboutPrograms)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AboutProgram_Program");
            });

            modelBuilder.Entity<AboutProgramOption>(entity =>
            {
                entity.HasKey(e => e.AboutProgramOptionsId);

                entity.HasIndex(e => e.ProductId, "IX_AboutProgramOptions_ProductId");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("{n}天內確認");

                entity.Property(e => e.IconClass).HasComment("icon圖標");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AboutProgramOptions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AboutProgramOptions_Product");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.ParentCategoryId, "IX_Category_ParentCategoryId");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("類別名稱");

                entity.Property(e => e.ParentCategoryId).HasComment("父類別");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasComment("");

                entity.Property(e => e.CityImage).IsRequired();

                entity.Property(e => e.CityIntrod)
                    .IsRequired()
                    .HasComment("城市說明");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("城市名稱");

                entity.Property(e => e.IsIsland).HasComment("是否為本島");

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("緯度");

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("經度");
            });

            modelBuilder.Entity<CityRaider>(entity =>
            {
                entity.HasKey(e => e.RaidersId);

                entity.HasIndex(e => e.CityId, "IX_CityRaiders_CityId");

                entity.HasIndex(e => e.StaffId, "IX_CityRaiders_StaffId");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .HasComment("卡牌用圖片");

                entity.Property(e => e.Isdeleted).HasComment("軟刪除");

                entity.Property(e => e.PostDate)
                    .HasColumnType("datetime")
                    .HasComment("發文時間");

                entity.Property(e => e.Status).HasComment("文章狀態");

                entity.Property(e => e.Subtitle).HasComment("副標題");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasComment("攻略內文");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasComment("主標題");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("更新時間(第一次發文存發文時間)");

                entity.Property(e => e.Video)
                    .IsRequired()
                    .HasComment("banner影片");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityRaiders)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityRaiders_City");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.CityRaiders)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityRaiders_Staff");
            });

            modelBuilder.Entity<Collect>(entity =>
            {
                entity.ToTable("Collect");

                entity.HasIndex(e => e.MemberId, "IX_Collect_MemberId");

                entity.HasIndex(e => e.ProductId, "IX_Collect_ProductId");

                entity.Property(e => e.CollectId).HasComment("收藏id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("加入時間");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Collects)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collect_Member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Collects)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collect_Product");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.MemberId, "IX_Comment_MemberId");

                entity.HasIndex(e => e.OrderDetailsId, "IX_Comment_OrderDetailsID");

                entity.HasIndex(e => e.ProductId, "IX_Comment_ProductId");

                entity.Property(e => e.CommentId).HasComment("評論");

                entity.Property(e => e.CommentDate)
                    .HasColumnType("datetime")
                    .HasComment("評論時間");

                entity.Property(e => e.CommentText)
                    .IsRequired()
                    .HasComment("評論內文");

                entity.Property(e => e.CommentTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("評論標題");

                entity.Property(e => e.MemberId).HasComment("會員id");

                entity.Property(e => e.OrderDetailsId)
                    .HasColumnName("OrderDetailsID")
                    .HasComment("詳細訂單ID");

                entity.Property(e => e.PartnerType).HasComment("旅伴類型ID");

                entity.Property(e => e.ProductId).HasComment("商品id");

                entity.Property(e => e.RatingStar).HasComment("幾星評價");

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

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Product");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.Context).HasComment("優惠卷簡易說明");

                entity.Property(e => e.CouponDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("折扣金額");

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("優惠卷名稱");

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("優惠碼");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasComment("結束日期");

                entity.Property(e => e.FullConsumption).HasComment("滿額 多少 (使用條件)");

                entity.Property(e => e.Rebate).HasComment("減價 多少 (使用條件)");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("開始日期");

                entity.Property(e => e.UseInfo).HasComment("使用條件");
            });

            modelBuilder.Entity<CouponManage>(entity =>
            {
                entity.ToTable("CouponManage");

                entity.HasIndex(e => e.CouponId, "IX_CouponManage_CouponId");

                entity.HasIndex(e => e.MemberId, "IX_CouponManage_MemberId");

                entity.HasIndex(e => e.StaffId, "IX_CouponManage_StaffId");

                entity.Property(e => e.CouponManageId).HasComment("優惠卷管理");

                entity.Property(e => e.CouponId).HasComment("優惠眷id");

                entity.Property(e => e.CouponStatus).HasComment("狀態");

                entity.Property(e => e.SendTime)
                    .HasColumnType("datetime")
                    .HasComment("發卷時間");

                entity.Property(e => e.StaffId).HasComment("員工發眷人");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponManages)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponManage_Coupon");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CouponManages)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponManage_Member");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.CouponManages)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponManage_Staff");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.HasIndex(e => e.ProductId, "IX_Location_ProductId");

                entity.Property(e => e.LocationId).HasComment("體驗地點ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("地點");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .HasComment("緯度");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .HasComment("經度");

                entity.Property(e => e.ProductId).HasComment("商品ID");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasComment("內文");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("體驗地點標題");

                entity.Property(e => e.Type).HasComment("類型0＝體驗 ,1(兌換),2...\r\n(地點種類)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Product");
            });

            modelBuilder.Entity<LoginWay>(entity =>
            {
                entity.ToTable("LoginWay");

                entity.HasIndex(e => e.MemberId, "IX_LoginWay_MemberID");

                entity.Property(e => e.LoginWayId).HasComment("登入方式ID");

                entity.Property(e => e.LongWayName).HasComment("登入方式(email1, fb2, google3)");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員ID");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UniqueID")
                    .HasComment("唯一ID (如果是EMAIL存EMAIL 若為三方登入給一個ID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.LoginWays)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginWay_Member");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.HasIndex(e => e.CityId, "IX_Member_CityId");

                entity.Property(e => e.MemberId).HasComment("會員ID");

                entity.Property(e => e.Age).HasComment("年齡");

                entity.Property(e => e.CityId).HasComment("城市ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("電子信箱");

                entity.Property(e => e.Gender).HasComment("性別");

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(10)
                    .HasComment("身分證字號");

                entity.Property(e => e.Image).HasComment("會員圖片");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(50)
                    .HasComment("會員名稱");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasComment("密碼");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasComment("電話");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Member_City");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.MemberId, "IX_Message_MemberId");

                entity.HasIndex(e => e.OrderId, "IX_Message_OrderId");

                entity.HasIndex(e => e.ReplyId, "IX_Message_ReplyId");

                entity.Property(e => e.MessageContext)
                    .IsRequired()
                    .HasComment("訊息內容");

                entity.Property(e => e.OrderId).HasComment("因為有訂單才能傳訊息");

                entity.Property(e => e.Recipient).HasComment("接受者(平台1 商家2 使用者3)");

                entity.Property(e => e.ReplyId).HasComment("回覆");

                entity.Property(e => e.SendDate)
                    .HasColumnType("datetime")
                    .HasComment("傳送時間");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Member");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Order");

                entity.HasOne(d => d.Reply)
                    .WithMany(p => p.InverseReply)
                    .HasForeignKey(d => d.ReplyId)
                    .HasConstraintName("FK_Message_Message");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => e.MemberId, "IX_Order_MemberId");

                entity.HasIndex(e => e.PaymentId, "IX_Order_PaymentId");

                entity.Property(e => e.OrderId).HasComment("訂單ID");

                entity.Property(e => e.Note).HasComment("備註");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasComment("下單日期");

                entity.Property(e => e.PaymentId).HasComment("付款ID");

                entity.Property(e => e.Status).HasComment("狀態(1.成功2.失敗3.已付款4.未付款)");

                entity.Property(e => e.StatusUpdate).HasComment("訂單狀態更新");

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
                entity.HasKey(e => e.OrderDetailsId)
                    .HasName("PK_OrderDetails");

                entity.ToTable("OrderDetail");

                entity.HasIndex(e => e.OrderId, "IX_OrderDetail_OrderId");

                entity.HasIndex(e => e.SpecificationId, "IX_OrderDetail_SpecificationId");

                entity.HasIndex(e => e.TicketsId, "IX_OrderDetail_TicketsId");

                entity.Property(e => e.OrderDetailsId).HasComment("訂單詳細ID");

                entity.Property(e => e.DepartureDate)
                    .HasColumnType("datetime")
                    .HasComment("出發日期");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("折扣");

                entity.Property(e => e.Itemtext)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("票種（成人/兒童/車/四人房/雙人房)");

                entity.Property(e => e.LeaseTime)
                    .HasColumnType("datetime")
                    .HasComment("租賃時間");

                entity.Property(e => e.OrderId).HasComment("訂單ID");

                entity.Property(e => e.Quantity).HasComment("數量");

                entity.Property(e => e.SpecificationId).HasComment("規格ID");

                entity.Property(e => e.TicketsId).HasComment("電子憑證ID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("價格");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Order");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_ProgramSpecification");

                entity.HasOne(d => d.Tickets)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.TicketsId)
                    .HasConstraintName("FK_OrderDetail_Ticket");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentWay)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("付款方式");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.CityId, "IX_Product_CityId");

                entity.HasIndex(e => e.SupplierId, "IX_Product_SupplierId");

                entity.Property(e => e.CancellationPolicy).HasComment("取消政策");

                entity.Property(e => e.HowUse).HasComment("如何使用");

                entity.Property(e => e.Illustrate).HasComment("商品說明");

                entity.Property(e => e.Isdeleted).HasComment("軟刪除");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("商品名稱");

                entity.Property(e => e.ShoppingNotice).HasComment("購物須知");

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

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.HasIndex(e => e.CategoryId, "IX_ProductCategory_CategoryId");

                entity.HasIndex(e => e.ProductId, "IX_ProductCategory_ProductId");

                entity.Property(e => e.ProductCategoryId).HasComment("商品類別");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategory_Category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategory_Product");
            });

            modelBuilder.Entity<ProductPhoto>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.ToTable("ProductPhoto");

                entity.HasIndex(e => e.ProductId, "IX_ProductPhoto_ProductId");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasComment("路徑");

                entity.Property(e => e.Sort).HasComment("照片排序");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPhotos)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPhoto_Product");
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.ToTable("ProductTag");

                entity.HasIndex(e => e.ProductId, "IX_ProductTag_ProductId");

                entity.HasIndex(e => e.TagId, "IX_ProductTag_TagId");

                entity.Property(e => e.ProductTagId).HasComment("商品標籤");

                entity.Property(e => e.ProductId).HasComment("商品id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTag_Product");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTag_Tag");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.HasIndex(e => e.ProductId, "IX_Program_ProductId");

                entity.Property(e => e.ProgramId).HasComment("");

                entity.Property(e => e.Context).HasComment("方案內文");

                entity.Property(e => e.Isdeleted).HasComment("軟刪除(上下架)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("方案標題");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Product");
            });

            modelBuilder.Entity<ProgramCantUseDate>(entity =>
            {
                entity.HasKey(e => e.ProgramDateId)
                    .HasName("PK_ProgramDatePicker");

                entity.ToTable("ProgramCantUseDate");

                entity.HasIndex(e => e.ProgramId, "IX_ProgramCantUseDate_ProgramID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasComment("要關閉的日期");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramCantUseDates)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramDatePicker_Program");
            });

            modelBuilder.Entity<ProgramInclude>(entity =>
            {
                entity.ToTable("ProgramInclude");

                entity.HasIndex(e => e.ProgramId, "IX_ProgramInclude_ProgramID");

                entity.Property(e => e.IsInclude).HasComment("是否包含(判斷放在哪邊)");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.Text).HasComment("內文");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramIncludes)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramInclude_Program");
            });

            modelBuilder.Entity<ProgramSpecification>(entity =>
            {
                entity.HasKey(e => e.SpecificationId);

                entity.ToTable("ProgramSpecification");

                entity.HasIndex(e => e.ProgramId, "IX_ProgramSpecification_ProgramId");

                entity.Property(e => e.Inventory).HasComment("庫存量");

                entity.Property(e => e.IsScreening).HasComment("有無場次");

                entity.Property(e => e.Itemtext)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("票種（成人/兒童/車)");

                entity.Property(e => e.OriginalUnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("原價");

                entity.Property(e => e.ProgramId).HasComment("方案ID");

                entity.Property(e => e.Status).HasComment("狀態(上下架 1上架2下架3缺貨?)");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("單價");

                entity.Property(e => e.UnitText)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("單位文字(人/間/輛）");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramSpecifications)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramSpecification_Program");
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.ToTable("Screening");

                entity.HasIndex(e => e.SpecificationId, "IX_Screening_SpecificationId");

                entity.Property(e => e.ScreeningId).HasComment("場次ID");

                entity.Property(e => e.Status).HasComment("狀態(上下架1上架2下架3缺貨?)");

                entity.Property(e => e.Time)
                    .HasMaxLength(200)
                    .HasComment("時間");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.SpecificationId)
                    .HasConstraintName("FK_Screening_ProgramSpecification");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.HasIndex(e => e.MemberId, "IX_ShoppingCart_MemberId");

                entity.HasIndex(e => e.ScreeningId, "IX_ShoppingCart_ScreeningId");

                entity.HasIndex(e => e.SpecificationId, "IX_ShoppingCart_SpecificationId");

                entity.Property(e => e.ShoppingCartId).HasComment("購物車ID");

                entity.Property(e => e.DepartureDate)
                    .HasColumnType("date")
                    .HasComment("出發日期");

                entity.Property(e => e.JoinTime)
                    .HasColumnType("datetime")
                    .HasComment("加入購物車時間");

                entity.Property(e => e.Quantity).HasComment("數量");

                entity.Property(e => e.ScreeningId).HasComment("場次");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppinCart_Member");

                entity.HasOne(d => d.Screening)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.ScreeningId)
                    .HasConstraintName("FK_ShoppingCart_Screening");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.SpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppinCart_ProgramSpecification");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription");

                entity.Property(e => e.SubscriptionId).HasComment("訂閱ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("email");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.HasIndex(e => e.CityId, "IX_Supplier_CityId");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasComment("供應商ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("公司地址");

                entity.Property(e => e.CityId).HasComment("城市");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("公司名稱");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("聯繫人姓名");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(50)
                    .HasComment("聯繫人職稱");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("電話");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_City");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.TagText)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("標籤名稱");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketsId)
                    .HasName("PK_Tickets");

                entity.ToTable("Ticket");

                entity.Property(e => e.TicketsId).HasComment("電子憑證ID");

                entity.Property(e => e.Status).HasComment("狀態(1未使用、2已使用)");

                entity.Property(e => e.TicketQrcode)
                    .IsRequired()
                    .HasColumnName("TicketQRcode")
                    .HasComment("qrcode");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).HasComment("員工ID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasComment("生日");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("員工姓名");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasComment("密碼");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("電話");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
