using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Today.Model.Models;
using TodayMVC.Admin.DTOModels.ProductDTO;

namespace TodayMVC.Admin.Repositories.DapperProductRepositories
{
    public class DapperProductRepository : DapperBaseRepository, IDapperProductRepository
    {
        public DapperProductRepository(IDbConnection conn):base(conn)
        {

        }
        public int Create(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public Product GetOne(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> SelectAll()
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<UpdateProductDTO.ProductInfo> SelectAllProduct()
        {
            var product = @"SELECT 
                            p.ProductId,p.ProductName,p.Illustrate,p.ShoppingNotice,p.CancellationPolicy,p.HowUse,
                            ct.CityId split_on,
                            ct.CityName,
                            c.CategoryId split_on,
                            c.CategoryName,
                            pp.PhotoId split_on,
                            pp.Path,
                            pg.ProgramId split_on,
                            pg.Title,pg.Context,
                            pcd.ProgramDateId split_on,
                            pcd.Date,
                            ps.SpecificationId split_on,
                            ps.UnitText,ps.UnitPrice,ps.Itemtext,
                            s.ShoppingCartId split_on,
                            s.DepartureDate
                            FROM Product AS p
                            INNER JOIN City AS ct ON(p.CityId = ct.CityId)
                            INNER JOIN ProductCategory AS pc ON(p.ProductId  = pc.ProductId)
                            INNER JOIN Category AS c ON(pc.CategoryId = c.CategoryId)
                            INNER JOIN ProductPhoto AS pp ON(p.ProductId = pp.ProductId)
                            INNER JOIN Program AS pg ON(p.ProductId = pg.ProductId)
                            left JOIN ProgramCantUseDate AS pcd ON(pg.ProgramId = pcd.ProgramId)
                            left JOIN ProgramSpecification AS ps ON(pg.ProgramId = ps.ProgramId)
                            left JOIN ShoppingCart AS s ON(ps.SpecificationId = s.SpecificationId)  
                            Where pp.Sort = 1";
            
            var result = _conn.Query<UpdateProductDTO.ProductInfo>(product);
            result.GroupBy(x => x.ProductId);
            

            //var result1 = _conn.Query<Product, City, Category, ProductPhoto, Today.Model.Models.Program, ProgramSpecification,ShoppingCart, Product>(
            //                product, (p, ct, c, pp, pg, ps, s) =>
            //                {
            //                    p.City = ct;
            //                    pg.Product = p;
            //                    pp.Product = p;
            //                    ps.Program = pg;
            //                    s.Specification = ps;
            //                    return p;
            //                }, splitOn: "split_on"
            //);
            
            //var product = @"SELECT * 
            //                FROM Product p
            //                left JOIN Location AS l ON(p.ProductId = l.ProductId)
            //                INNER JOIN City AS ct ON(p.CityId = ct.CityId)
            //                INNER JOIN ProductCategory AS pc ON(p.ProductId  = pc.ProductId)
            //                INNER JOIN Category AS c ON(pc.CategoryId = c.CategoryId)
            //                INNER JOIN ProductPhoto AS pp ON(p.ProductId = pp.ProductId)
            //                INNER JOIN Program AS pg ON(p.ProductId = pg.ProductId)
            //                left JOIN ProgramSpecification AS ps ON(pg.ProgramId = ps.ProgramId)
            //                left JOIN ShoppingCart AS s ON(ps.SpecificationId = s.SpecificationId)";

            //var result = _conn.Query<Product>(product);

            return result;

        }

        public int Update(Product entity)
        {
            
            var query = $"UPDATE Product SET ProductName = @ProductName WHERE ProductId = @ProductId";

            return _conn.Execute(query, new {ProductId = entity.ProductId, ProductName = entity.ProductName});
        }
    }
}
