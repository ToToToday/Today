using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Today.Model.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "類別名稱"),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true, comment: "父類別")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "城市名稱"),
                    CityIntrod = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "城市說明"),
                    IsIsland = table.Column<bool>(type: "bit", nullable: false, comment: "是否為本島"),
                    CityImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false),
                    CouponName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "優惠卷名稱"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "開始日期"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "結束日期"),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "優惠卷簡易說明"),
                    DiscountCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "優惠碼"),
                    CouponDiscount = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "折扣金額"),
                    FullConsumption = table.Column<int>(type: "int", nullable: true, comment: "滿額 多少 (使用條件)"),
                    Rebate = table.Column<int>(type: "int", nullable: true, comment: "減價 多少 (使用條件)"),
                    UseInfo = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "使用條件")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    PaymentWay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "付款方式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false, comment: "員工ID"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "員工姓名"),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "電話"),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true, comment: "生日"),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "密碼")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false, comment: "訂閱ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "email")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    TagText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "標籤名稱")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketsId = table.Column<int>(type: "int", nullable: false, comment: "電子憑證ID"),
                    TicketQRcode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "qrcode"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "狀態")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketsId);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false, comment: "會員ID"),
                    MemberName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "會員名稱"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "城市ID"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "會員圖片"),
                    Age = table.Column<int>(type: "int", nullable: true, comment: "年齡"),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "電話"),
                    IdentityCard = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "身分證字號"),
                    Gender = table.Column<bool>(type: "bit", nullable: true, comment: "性別"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "密碼"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "電子信箱")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Member_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false, comment: "供應商ID"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "公司名稱"),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "聯繫人姓名"),
                    ContactTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "聯繫人職稱"),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "公司地址"),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "電話"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "城市")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                    table.ForeignKey(
                        name: "FK_Supplier_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityRaiders",
                columns: table => new
                {
                    RaidersId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "主標題"),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "副標題"),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "banner影片"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "攻略內文"),
                    PostDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "發文時間"),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "更新時間(第一次發文存發文時間)"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "文章狀態"),
                    Isdeleted = table.Column<bool>(type: "bit", nullable: false, comment: "軟刪除"),
                    StaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityRaiders", x => x.RaidersId);
                    table.ForeignKey(
                        name: "FK_CityRaiders_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityRaiders_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CouponManage",
                columns: table => new
                {
                    CouponManageId = table.Column<int>(type: "int", nullable: false, comment: "優惠卷管理"),
                    CouponId = table.Column<int>(type: "int", nullable: false, comment: "優惠眷id"),
                    StaffId = table.Column<int>(type: "int", nullable: false, comment: "員工發眷人"),
                    SendTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "發卷時間"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CouponStatus = table.Column<int>(type: "int", nullable: false, comment: "狀態")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponManage", x => x.CouponManageId);
                    table.ForeignKey(
                        name: "FK_CouponManage_Coupon",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "CouponId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouponManage_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouponManage_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginWay",
                columns: table => new
                {
                    LoginWayId = table.Column<int>(type: "int", nullable: false, comment: "登入方式ID"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員ID"),
                    LongWayName = table.Column<int>(type: "int", nullable: false, comment: "登入方式(email1, fb2, google3)"),
                    UniqueID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "唯一ID (如果是EMAIL存EMAIL 若為三方登入給一個ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginWay", x => x.LoginWayId);
                    table.ForeignKey(
                        name: "FK_LoginWay_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "訂單ID"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "下單日期"),
                    PaymentId = table.Column<int>(type: "int", nullable: false, comment: "付款ID"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "狀態"),
                    StatusUpdate = table.Column<int>(type: "int", nullable: false, comment: "訂單狀態更新"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Payment",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "商品名稱"),
                    Illustrate = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "商品說明"),
                    ShoppingNotice = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "購物須知"),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "取消政策"),
                    HowUse = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "如何使用"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Isdeleted = table.Column<bool>(type: "bit", nullable: false, comment: "軟刪除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Supplier",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageContext = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "訊息內容"),
                    SendDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "傳送時間"),
                    Recipient = table.Column<int>(type: "int", nullable: false, comment: "接受者(平台1 商家2 使用者3)"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "因為有訂單才能傳訊息"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ReplyId = table.Column<int>(type: "int", nullable: true, comment: "回覆")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Message",
                        column: x => x.ReplyId,
                        principalTable: "Message",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Order",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AboutProgramOptions",
                columns: table => new
                {
                    AboutProgramOptionsId = table.Column<int>(type: "int", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "{n}天內確認"),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "icon圖標"),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutProgramOptions", x => x.AboutProgramOptionsId);
                    table.ForeignKey(
                        name: "FK_AboutProgramOptions_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collect",
                columns: table => new
                {
                    CollectId = table.Column<int>(type: "int", nullable: false, comment: "收藏id"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "加入時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collect", x => x.CollectId);
                    table.ForeignKey(
                        name: "FK_Collect_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collect_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false, comment: "體驗地點ID"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "商品ID"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "體驗地點標題"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "內文"),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "地點"),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "經度"),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "緯度"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "類型0＝體驗 ,1,2...\r\n(地點種類)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false, comment: "商品類別"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPhoto",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "路徑"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "照片排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhoto", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_ProductPhoto_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductTagId = table.Column<int>(type: "int", nullable: false, comment: "商品標籤"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "商品id"),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => x.ProductTagId);
                    table.ForeignKey(
                        name: "FK_ProductTag_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tag",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false, comment: ""),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "方案標題"),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "方案內文"),
                    Isdeleted = table.Column<bool>(type: "bit", nullable: false, comment: "軟刪除(上下架)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Program_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AboutProgram",
                columns: table => new
                {
                    AboutProgramId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    AboutProgramOptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutProgram", x => x.AboutProgramId);
                    table.ForeignKey(
                        name: "FK_AboutProgram_AboutProgramOptions",
                        column: x => x.AboutProgramOptionsId,
                        principalTable: "AboutProgramOptions",
                        principalColumn: "AboutProgramOptionsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AboutProgram_Program",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramCantUseDate",
                columns: table => new
                {
                    ProgramDateId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false, comment: "要關閉的日期"),
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDatePicker", x => x.ProgramDateId);
                    table.ForeignKey(
                        name: "FK_ProgramDatePicker_Program",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramInclude",
                columns: table => new
                {
                    ProgramIncludeId = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "內文"),
                    IsInclude = table.Column<bool>(type: "bit", nullable: true, comment: "是否包含(判斷放在哪邊)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramInclude", x => x.ProgramIncludeId);
                    table.ForeignKey(
                        name: "FK_ProgramInclude_Program",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSpecification",
                columns: table => new
                {
                    SpecificationId = table.Column<int>(type: "int", nullable: false),
                    IsScreening = table.Column<bool>(type: "bit", nullable: false, comment: "有無場次"),
                    ProgramId = table.Column<int>(type: "int", nullable: false, comment: "方案ID"),
                    Itemtext = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "票種（成人/兒童/車)"),
                    UnitText = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "單位文字(人/間/輛）"),
                    OriginalUnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "原價"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "單價"),
                    Inventory = table.Column<int>(type: "int", nullable: true, comment: "庫存量"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "狀態(上下架)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSpecification", x => x.SpecificationId);
                    table.ForeignKey(
                        name: "FK_ProgramSpecification_Program",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailsId = table.Column<int>(type: "int", nullable: false, comment: "訂單詳細ID"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "訂單ID"),
                    SpecificationId = table.Column<int>(type: "int", nullable: false, comment: "規格ID"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "數量"),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: true, comment: "折扣"),
                    Itemtext = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "票種（成人/兒童/車)"),
                    LeaseTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "租賃時間"),
                    TicketsId = table.Column<int>(type: "int", nullable: false, comment: "電子憑證ID"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "價格"),
                    DepartureDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "出發日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailsId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_ProgramSpecification",
                        column: x => x.SpecificationId,
                        principalTable: "ProgramSpecification",
                        principalColumn: "SpecificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Ticket",
                        column: x => x.TicketsId,
                        principalTable: "Ticket",
                        principalColumn: "TicketsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Screening",
                columns: table => new
                {
                    ScreeningId = table.Column<int>(type: "int", nullable: false, comment: "場次ID"),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false, comment: "時間"),
                    SpecificationId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "狀態(上下架)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screening", x => x.ScreeningId);
                    table.ForeignKey(
                        name: "FK_Screening_ProgramSpecification",
                        column: x => x.SpecificationId,
                        principalTable: "ProgramSpecification",
                        principalColumn: "SpecificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false, comment: "評論"),
                    OrderDetailsID = table.Column<int>(type: "int", nullable: false, comment: "詳細訂單ID"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "商品id"),
                    MemberId = table.Column<int>(type: "int", nullable: false, comment: "會員id"),
                    PartnerType = table.Column<int>(type: "int", nullable: false, comment: "旅伴類型ID"),
                    CommentDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "評論時間"),
                    RatingStar = table.Column<int>(type: "int", nullable: false, comment: "幾星評價"),
                    CommentTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "評論標題"),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "評論內文")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_OrderDetails",
                        column: x => x.OrderDetailsID,
                        principalTable: "OrderDetail",
                        principalColumn: "OrderDetailsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false, comment: "購物車ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SpecificationId = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "date", nullable: false, comment: "出發日期"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "數量"),
                    ScreeningId = table.Column<int>(type: "int", nullable: false, comment: "場次"),
                    JoinTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "加入購物車時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_ShoppinCart_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppinCart_ProgramSpecification",
                        column: x => x.SpecificationId,
                        principalTable: "ProgramSpecification",
                        principalColumn: "SpecificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Screening",
                        column: x => x.ScreeningId,
                        principalTable: "Screening",
                        principalColumn: "ScreeningId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutProgram_AboutProgramOptionsId",
                table: "AboutProgram",
                column: "AboutProgramOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutProgram_ProgramId",
                table: "AboutProgram",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutProgramOptions_ProductId",
                table: "AboutProgramOptions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CityRaiders_CityId",
                table: "CityRaiders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CityRaiders_StaffId",
                table: "CityRaiders",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_MemberId",
                table: "Collect",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_ProductId",
                table: "Collect",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MemberId",
                table: "Comment",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OrderDetailsID",
                table: "Comment",
                column: "OrderDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductId",
                table: "Comment",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponManage_CouponId",
                table: "CouponManage",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponManage_MemberId",
                table: "CouponManage",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponManage_StaffId",
                table: "CouponManage",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ProductId",
                table: "Location",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginWay_MemberID",
                table: "LoginWay",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CityId",
                table: "Member",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_MemberId",
                table: "Message",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_OrderId",
                table: "Message",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReplyId",
                table: "Message",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MemberId",
                table: "Order",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentId",
                table: "Order",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_SpecificationId",
                table: "OrderDetail",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_TicketsId",
                table: "OrderDetail",
                column: "TicketsId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CityId",
                table: "Product",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierId",
                table: "Product",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhoto_ProductId",
                table: "ProductPhoto",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_ProductId",
                table: "ProductTag",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagId",
                table: "ProductTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_ProductId",
                table: "Program",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCantUseDate_ProgramID",
                table: "ProgramCantUseDate",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInclude_ProgramID",
                table: "ProgramInclude",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSpecification_ProgramId",
                table: "ProgramSpecification",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Screening_SpecificationId",
                table: "Screening",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_MemberId",
                table: "ShoppingCart",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ScreeningId",
                table: "ShoppingCart",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_SpecificationId",
                table: "ShoppingCart",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CityId",
                table: "Supplier",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutProgram");

            migrationBuilder.DropTable(
                name: "CityRaiders");

            migrationBuilder.DropTable(
                name: "Collect");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CouponManage");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "LoginWay");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductPhoto");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "ProgramCantUseDate");

            migrationBuilder.DropTable(
                name: "ProgramInclude");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "AboutProgramOptions");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Screening");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProgramSpecification");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
