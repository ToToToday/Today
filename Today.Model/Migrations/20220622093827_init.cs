using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Today.Model.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutProgramOptions",
                columns: table => new
                {
                    AboutProgramOptionsID = table.Column<int>(type: "int", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutProgramOptions", x => x.AboutProgramOptionsID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityImg = table.Column<byte[]>(type: "image", nullable: false),
                    CityIntrod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsIsland = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponID = table.Column<int>(type: "int", nullable: false),
                    CouponName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CouponDiscount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ConditionsOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.CouponID);
                });

            migrationBuilder.CreateTable(
                name: "PartnerType",
                columns: table => new
                {
                    PartnerTypeID = table.Column<int>(type: "int", nullable: false),
                    PartnerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerType", x => x.PartnerTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    PaymentWay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryCategory",
                columns: table => new
                {
                    PrimaryCategoryID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryCategory", x => x.PrimaryCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionID);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketsID = table.Column<int>(type: "int", nullable: false),
                    TicketsQRcode = table.Column<byte[]>(type: "image", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketsID);
                });

            migrationBuilder.CreateTable(
                name: "CityRaiders",
                columns: table => new
                {
                    RaidersID = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityRaiders", x => x.RaidersID);
                    table.ForeignKey(
                        name: "FK_CityRaiders_City",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                    table.ForeignKey(
                        name: "FK_Supplier_City",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinorCategory",
                columns: table => new
                {
                    MinorCategoryID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimaryCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorCategory", x => x.MinorCategoryID);
                    table.ForeignKey(
                        name: "FK_MinorCategory_PrimaryCategory",
                        column: x => x.PrimaryCategoryID,
                        principalTable: "PrimaryCategory",
                        principalColumn: "PrimaryCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UseInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Illustrate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShoppingNotice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockMax = table.Column<int>(type: "int", nullable: false),
                    UnitsOnOrder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HowUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_City",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Supplier",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    MinorCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Category_MinorCategory",
                        column: x => x.MinorCategoryID,
                        principalTable: "MinorCategory",
                        principalColumn: "MinorCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_Location_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductStatus",
                columns: table => new
                {
                    ProductStatusID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    RemainingStock = table.Column<int>(type: "int", nullable: false),
                    BookQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatus", x => x.ProductStatusID);
                    table.ForeignKey(
                        name: "FK_ProductStatus_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_Program_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false),
                    TagText = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                    table.ForeignKey(
                        name: "FK_Tag_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AboutProgram",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    AboutProgramOptionsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AboutProgram_AboutProgramOptions",
                        column: x => x.AboutProgramOptionsID,
                        principalTable: "AboutProgramOptions",
                        principalColumn: "AboutProgramOptionsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AboutProgram_Program",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDatePicker",
                columns: table => new
                {
                    ProgramDateID = table.Column<int>(type: "int", nullable: false),
                    DatimePickerConfigurationJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDatePicker", x => x.ProgramDateID);
                    table.ForeignKey(
                        name: "FK_ProgramDatePicker_Program",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramInclude",
                columns: table => new
                {
                    ProgramIncludeID = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncludeTorF = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ProgramInclude_Program",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSpecification",
                columns: table => new
                {
                    SpecificationID = table.Column<int>(type: "int", nullable: false),
                    SpecificationJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSpecification", x => x.SpecificationID);
                    table.ForeignKey(
                        name: "FK_ProgramSpecification_Program",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PricingItems",
                columns: table => new
                {
                    PricingItemID = table.Column<int>(type: "int", nullable: false),
                    SpecificationID = table.Column<int>(type: "int", nullable: false),
                    Itemtext = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitText = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OriginalUnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingItems", x => x.PricingItemID);
                    table.ForeignKey(
                        name: "FK_PricingItems_ProgramSpecification",
                        column: x => x.SpecificationID,
                        principalTable: "ProgramSpecification",
                        principalColumn: "SpecificationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdentityCard = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MessageID = table.Column<int>(type: "int", nullable: true),
                    CouponID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Member_City",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_Coupon",
                        column: x => x.CouponID,
                        principalTable: "Coupon",
                        principalColumn: "CouponID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collect",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Collect_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collect_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginWay",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    LongWayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UniqueID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_LoginWay_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Payment",
                        column: x => x.PaymentID,
                        principalTable: "Payment",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppinCart",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    SpecificationID = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ShoppinCart_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppinCart_ProgramSpecification",
                        column: x => x.SpecificationID,
                        principalTable: "ProgramSpecification",
                        principalColumn: "SpecificationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    MessageContext = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailsID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    DetailJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaseTime = table.Column<DateTime>(type: "date", nullable: false),
                    TicketsID = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailsID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Tickets",
                        column: x => x.TicketsID,
                        principalTable: "Tickets",
                        principalColumn: "TicketsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    ReplyID = table.Column<int>(type: "int", nullable: false),
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    ReplayText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.ReplyID);
                    table.ForeignKey(
                        name: "FK_Reply_Message",
                        column: x => x.MessageID,
                        principalTable: "Message",
                        principalColumn: "MessageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false),
                    OrderDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    PartnerTypeID = table.Column<int>(type: "int", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "date", nullable: false),
                    RatingStar = table.Column<int>(type: "int", nullable: false),
                    CommentTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_OrderDetails",
                        column: x => x.OrderDetailsID,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderDetailsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_PartnerType",
                        column: x => x.PartnerTypeID,
                        principalTable: "PartnerType",
                        principalColumn: "PartnerTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutProgram_AboutProgramOptionsID",
                table: "AboutProgram",
                column: "AboutProgramOptionsID");

            migrationBuilder.CreateIndex(
                name: "IX_AboutProgram_ProgramID",
                table: "AboutProgram",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_MinorCategoryID",
                table: "Category",
                column: "MinorCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ProductID",
                table: "Category",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CityRaiders_CityID",
                table: "CityRaiders",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_MemberID",
                table: "Collect",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_ProductID",
                table: "Collect",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MemberID",
                table: "Comment",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OrderDetailsID",
                table: "Comment",
                column: "OrderDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PartnerTypeID",
                table: "Comment",
                column: "PartnerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductID",
                table: "Comment",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ProductID",
                table: "Location",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_LoginWay_MemberID",
                table: "LoginWay",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CityID",
                table: "Member",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CouponID",
                table: "Member",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_MessageID",
                table: "Member",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_OrderID",
                table: "Message",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_MinorCategory_PrimaryCategoryID",
                table: "MinorCategory",
                column: "PrimaryCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MemberID",
                table: "Order",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentID",
                table: "Order",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_TicketsID",
                table: "OrderDetails",
                column: "TicketsID");

            migrationBuilder.CreateIndex(
                name: "IX_PricingItems_SpecificationID",
                table: "PricingItems",
                column: "SpecificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CityID",
                table: "Product",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierID",
                table: "Product",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStatus_ProductID",
                table: "ProductStatus",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Program_ProductID",
                table: "Program",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDatePicker_ProgramID",
                table: "ProgramDatePicker",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInclude_ProgramID",
                table: "ProgramInclude",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSpecification_ProgramID",
                table: "ProgramSpecification",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_MessageID",
                table: "Reply",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppinCart_MemberID",
                table: "ShoppinCart",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppinCart_SpecificationID",
                table: "ShoppinCart",
                column: "SpecificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CityID",
                table: "Supplier",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ProductID",
                table: "Tag",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Message",
                table: "Member",
                column: "MessageID",
                principalTable: "Message",
                principalColumn: "MessageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_City",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Member",
                table: "Order");

            migrationBuilder.DropTable(
                name: "AboutProgram");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CityRaiders");

            migrationBuilder.DropTable(
                name: "Collect");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "LoginWay");

            migrationBuilder.DropTable(
                name: "PricingItems");

            migrationBuilder.DropTable(
                name: "ProductStatus");

            migrationBuilder.DropTable(
                name: "ProgramDatePicker");

            migrationBuilder.DropTable(
                name: "ProgramInclude");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "ShoppinCart");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "AboutProgramOptions");

            migrationBuilder.DropTable(
                name: "MinorCategory");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PartnerType");

            migrationBuilder.DropTable(
                name: "ProgramSpecification");

            migrationBuilder.DropTable(
                name: "PrimaryCategory");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
