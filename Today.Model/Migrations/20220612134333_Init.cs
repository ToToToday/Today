using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Today.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    AdID = table.Column<int>(type: "int", nullable: false),
                    AdName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Peroid = table.Column<DateTime>(type: "date", nullable: false),
                    Notice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.AdID);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    CarModelID = table.Column<int>(type: "int", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.CarModelID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityIntrod = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    CouponName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.CouponID);
                });

            migrationBuilder.CreateTable(
                name: "HowUseDetails",
                columns: table => new
                {
                    HowUseDetailsID = table.Column<int>(type: "int", nullable: false),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowUseDetails", x => x.HowUseDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false),
                    InvoiceWay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceID);
                });

            migrationBuilder.CreateTable(
                name: "LocationDetails",
                columns: table => new
                {
                    LocationDetailsID = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationDetails", x => x.LocationDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "LoginWay",
                columns: table => new
                {
                    LoginWayID = table.Column<int>(type: "int", nullable: false),
                    LoginWayName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginWay", x => x.LoginWayID);
                });

            migrationBuilder.CreateTable(
                name: "PamerType",
                columns: table => new
                {
                    PamerTypeID = table.Column<int>(type: "int", nullable: false),
                    PamerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PamerType", x => x.PamerTypeID);
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
                name: "PromotionWay",
                columns: table => new
                {
                    PromotionWayID = table.Column<int>(type: "int", nullable: false),
                    PromotionWayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionWay", x => x.PromotionWayID);
                });

            migrationBuilder.CreateTable(
                name: "StoreMessage",
                columns: table => new
                {
                    StoreMessageID = table.Column<int>(type: "int", nullable: false),
                    MessageContext = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMessage", x => x.StoreMessageID);
                });

            migrationBuilder.CreateTable(
                name: "TagDetails",
                columns: table => new
                {
                    TagDetailsID = table.Column<int>(type: "int", nullable: false),
                    TagInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.TagDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false),
                    TicketsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                });

            migrationBuilder.CreateTable(
                name: "TodayMessage",
                columns: table => new
                {
                    TodayMessageID = table.Column<int>(type: "int", nullable: false),
                    MessageContext = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodayMessage", x => x.TodayMessageID);
                });

            migrationBuilder.CreateTable(
                name: "CityRaiders",
                columns: table => new
                {
                    RaidersID = table.Column<int>(type: "int", nullable: false),
                    CItyID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Video = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityRaiders", x => x.RaidersID);
                    table.ForeignKey(
                        name: "FK_CityRaiders_City",
                        column: x => x.CItyID,
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
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
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
                name: "CouponDetails",
                columns: table => new
                {
                    CouponDetailsID = table.Column<int>(type: "int", nullable: false),
                    CouponDiscount = table.Column<int>(type: "int", nullable: false),
                    CouponID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponDetails", x => x.CouponDetailsID);
                    table.ForeignKey(
                        name: "FK_CouponDetails_Coupon",
                        column: x => x.CouponID,
                        principalTable: "Coupon",
                        principalColumn: "CouponID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberInfoID",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: true),
                    Brithday = table.Column<DateTime>(type: "date", nullable: false),
                    Pchone = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginWayID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberInfoID", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_MemberInfoID_City",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberInfoID_LoginWay",
                        column: x => x.LoginWayID,
                        principalTable: "LoginWay",
                        principalColumn: "LoginWayID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinorCategory",
                columns: table => new
                {
                    MinorCategoryID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
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
                name: "Offers",
                columns: table => new
                {
                    OffersID = table.Column<int>(type: "int", nullable: false),
                    AdID = table.Column<int>(type: "int", nullable: true),
                    PromotionWayID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OffersID);
                    table.ForeignKey(
                        name: "FK_Offers_Ad",
                        column: x => x.AdID,
                        principalTable: "Ad",
                        principalColumn: "AdID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_PromotionWay",
                        column: x => x.PromotionWayID,
                        principalTable: "PromotionWay",
                        principalColumn: "PromotionWayID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    TodayMessageID = table.Column<int>(type: "int", nullable: true),
                    StoreMessageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_StoreMessage",
                        column: x => x.StoreMessageID,
                        principalTable: "StoreMessage",
                        principalColumn: "StoreMessageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_TodayMessage",
                        column: x => x.TodayMessageID,
                        principalTable: "TodayMessage",
                        principalColumn: "TodayMessageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "date", nullable: true),
                    PaymentID = table.Column<int>(type: "int", nullable: true),
                    InvoiceID = table.Column<int>(type: "int", nullable: true),
                    LeaseTime = table.Column<DateTime>(type: "date", nullable: true),
                    SumPrice = table.Column<decimal>(type: "money", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Invoice",
                        column: x => x.InvoiceID,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_MemberInfoID",
                        column: x => x.MemberID,
                        principalTable: "MemberInfoID",
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
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UseInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShoppingNotice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Illustrate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    MinorCategoryID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Product_MinorCategory",
                        column: x => x.MinorCategoryID,
                        principalTable: "MinorCategory",
                        principalColumn: "MinorCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Supplier",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collect",
                columns: table => new
                {
                    CollectID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collect", x => x.CollectID);
                    table.ForeignKey(
                        name: "FK_Collect_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ParnerTypeID = table.Column<int>(type: "int", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "date", nullable: false),
                    RatingStar = table.Column<int>(type: "int", nullable: false),
                    CommentTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_MemberInfoID",
                        column: x => x.MemberID,
                        principalTable: "MemberInfoID",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_PamerType",
                        column: x => x.ParnerTypeID,
                        principalTable: "PamerType",
                        principalColumn: "PamerTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventLocation",
                columns: table => new
                {
                    EventLocationID = table.Column<int>(type: "int", nullable: false),
                    LocationDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocation", x => x.EventLocationID);
                    table.ForeignKey(
                        name: "FK_EventLocation_LocationDetails",
                        column: x => x.LocationDetailsID,
                        principalTable: "LocationDetails",
                        principalColumn: "LocationDetailsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLocation_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HowUse",
                columns: table => new
                {
                    HowUseID = table.Column<int>(type: "int", nullable: false),
                    HowUseDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowUse", x => x.HowUseID);
                    table.ForeignKey(
                        name: "FK_HowUse_HowUseDetails",
                        column: x => x.HowUseDetailsID,
                        principalTable: "HowUseDetails",
                        principalColumn: "HowUseDetailsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HowUse_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OffersDetails",
                columns: table => new
                {
                    OffersDetailsID = table.Column<int>(type: "int", nullable: false),
                    OffersID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    DiscountAngin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffersDetails", x => x.OffersDetailsID);
                    table.ForeignKey(
                        name: "FK_OffersDetails_Offers",
                        column: x => x.OffersID,
                        principalTable: "Offers",
                        principalColumn: "OffersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OffersDetails_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetais",
                columns: table => new
                {
                    OrderDetailsID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TicketsID = table.Column<int>(type: "int", nullable: true),
                    VehiclesNumber = table.Column<int>(type: "int", nullable: true),
                    CarModelID = table.Column<int>(type: "int", nullable: true),
                    Seats = table.Column<int>(type: "int", nullable: true),
                    ShowTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetais", x => x.OrderDetailsID);
                    table.ForeignKey(
                        name: "FK_OrderDetais_CarModel",
                        column: x => x.CarModelID,
                        principalTable: "CarModel",
                        principalColumn: "CarModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetais_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetais_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetais_Tickets",
                        column: x => x.TicketsID,
                        principalTable: "Tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false),
                    TagDetailsID = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Tag_TagDetails",
                        column: x => x.TagDetailsID,
                        principalTable: "TagDetails",
                        principalColumn: "TagDetailsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    CollectID = table.Column<int>(type: "int", nullable: false),
                    CouponID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Member_Collect",
                        column: x => x.CollectID,
                        principalTable: "Collect",
                        principalColumn: "CollectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_Coupon",
                        column: x => x.CouponID,
                        principalTable: "Coupon",
                        principalColumn: "CouponID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_MemberInfoID",
                        column: x => x.MemberID,
                        principalTable: "MemberInfoID",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_Message",
                        column: x => x.MessageID,
                        principalTable: "Message",
                        principalColumn: "MessageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityRaiders_CItyID",
                table: "CityRaiders",
                column: "CItyID");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_ProductID",
                table: "Collect",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MemberID",
                table: "Comment",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OrderID",
                table: "Comment",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParnerTypeID",
                table: "Comment",
                column: "ParnerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductID",
                table: "Comment",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CouponDetails_CouponID",
                table: "CouponDetails",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_LocationDetailsID",
                table: "EventLocation",
                column: "LocationDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_ProductID",
                table: "EventLocation",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_HowUse_HowUseDetailsID",
                table: "HowUse",
                column: "HowUseDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_HowUse_ProductID",
                table: "HowUse",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CollectID",
                table: "Member",
                column: "CollectID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CouponID",
                table: "Member",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_MemberID",
                table: "Member",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_MessageID",
                table: "Member",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberInfoID_CityID",
                table: "MemberInfoID",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberInfoID_LoginWayID",
                table: "MemberInfoID",
                column: "LoginWayID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_StoreMessageID",
                table: "Message",
                column: "StoreMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_TodayMessageID",
                table: "Message",
                column: "TodayMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_MinorCategory_PrimaryCategoryID",
                table: "MinorCategory",
                column: "PrimaryCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AdID",
                table: "Offers",
                column: "AdID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PromotionWayID",
                table: "Offers",
                column: "PromotionWayID");

            migrationBuilder.CreateIndex(
                name: "IX_OffersDetails_OffersID",
                table: "OffersDetails",
                column: "OffersID");

            migrationBuilder.CreateIndex(
                name: "IX_OffersDetails_ProductID",
                table: "OffersDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_InvoiceID",
                table: "Order",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MemberID",
                table: "Order",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentID",
                table: "Order",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetais_CarModelID",
                table: "OrderDetais",
                column: "CarModelID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetais_OrderID",
                table: "OrderDetais",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetais_ProductID",
                table: "OrderDetais",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetais_TicketsID",
                table: "OrderDetais",
                column: "TicketsID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CityID",
                table: "Product",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MinorCategoryID",
                table: "Product",
                column: "MinorCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierID",
                table: "Product",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CityID",
                table: "Supplier",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ProductID",
                table: "Tag",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagDetailsID",
                table: "Tag",
                column: "TagDetailsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityRaiders");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CouponDetails");

            migrationBuilder.DropTable(
                name: "EventLocation");

            migrationBuilder.DropTable(
                name: "HowUse");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "OffersDetails");

            migrationBuilder.DropTable(
                name: "OrderDetais");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "PamerType");

            migrationBuilder.DropTable(
                name: "LocationDetails");

            migrationBuilder.DropTable(
                name: "HowUseDetails");

            migrationBuilder.DropTable(
                name: "Collect");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TagDetails");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "StoreMessage");

            migrationBuilder.DropTable(
                name: "TodayMessage");

            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "PromotionWay");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "MemberInfoID");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "MinorCategory");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "LoginWay");

            migrationBuilder.DropTable(
                name: "PrimaryCategory");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
