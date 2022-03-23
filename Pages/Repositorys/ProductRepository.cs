using System.Data;
using Dapper;
using nhl_stenden_cafe.Pages.Models;

namespace nhl_stenden_cafe.Pages.Repository
{
    public class ProductRepository
    {
        /// <summary>
        /// sets up connection with the database
        /// </summary>
        /// <returns></returns>
        private static IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        /// <summary>
        /// adding a new product
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="catid"></param>
        /// <param name="price"></param>

        public void Add(string Name, int catid, decimal price)
        {

            string sql = @"
                INSERT INTO Product (Name, CategoryId, Price) 
                VALUES (@Name, @catid, @price);";

            using var connection = GetConnection();
            var addedEntry = connection.Execute(sql, new {Name, catid, price });
        }

        public void Update(int prodid, string Name, int catid, decimal price)
        {
            string sql = @"UPDATE product SET Name = @Name, Price = @price, CategoryId = @catid WHERE ProductId = @prodid";
            using var connection = GetConnection();
            connection.Execute(sql, new { prodid, Name, catid, price});
        }

        /// <summary>
        /// gets all products
        /// </summary>
        /// <returns>returns an IEnumerable containing all products</returns>
        public IEnumerable<Product> Get()
        {

            //the query is made here.
            string sql = @"select * 
                from product p 
                inner join category c on p.categoryid = c.categoryid";

            using var connection = GetConnection();
            //multimapping
            var products = connection.Query<Product, Category, Product>(sql, (product, category) =>
                {
                    product.Category = category;
                    return product;
                }, splitOn: "CategoryId");
                return products;
        }

        /// <summary>
        /// deletes a single product
        /// </summary>
        /// <param name="productId">the product id</param>
        public void DeleteSingle(int productId)
        {
           var orderRepository = new OrderRepository();
            orderRepository.DeleteAllSpecific(productId);
            //the query
            string sql = @"DELETE FROM product WHERE ProductId = @productId";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new { productId });
        }



        /// <summary>
        /// get products by id of category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetByCatid(int categoryId)
        {
            var p = new
            {
                CategoryID = categoryId
            };

            string sql = @"select p.productid, p.name, p.categoryid, c.name 
                from product p 
                inner join category c on p.categoryid = c.categoryid
                where p.categoryid = @CategoryID";

            using var connection = GetConnection();
            //var products = connection.Query<Product>(sql);





            var products = connection.Query<Product, Category, Product>(sql, (product, category) =>
            {
                product.Category = category;
                return product;
            },p, splitOn: "CategoryId");
            return products;

        }
        /// <summary>
        /// gets product by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Product Get(int id)
        {
            string sql = "SELECT * FROM product WHERE ProductId = @id";

            using var connection = GetConnection();
            Product product = connection.QuerySingle<Product>(sql, new { id });
            return product;
        }
        public Product count(int id)
        {
            string sql = "SELECT * FROM product WHERE ProductId = @id";

            using var connection = GetConnection();
            Product product = connection.QuerySingle<Product>(sql, new { id });
            return product;
        }
    }
}
