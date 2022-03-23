using Dapper;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Models;
using nhl_stenden_cafe.Pages.Repositorys;
using System.Data;

namespace nhl_stenden_cafe.Pages.Repository
{
    public class OrderRepository
    {
        private static IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        /// <summary>
        /// adds single order entry to the db
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="OrderId"></param>
        public void AddEntry(int ProductId, string OrderId)
        {

            string sql = @"
                INSERT INTO orderentrys (productid, orderid) 
                VALUES (@ProductId, @OrderId);";

            using var connection = GetConnection();
                var addedEntry = connection.Execute(sql, new { ProductId, OrderId });
        }


        /// <summary>
        /// adds single order to the  db
        /// </summary>
        /// <param name="orderId"> the order id</param>
        /// <param name="tafelNr">the table number</param>
        public void AddOrder(string orderId, int tafelNr)
        {

            string sql = @"
                INSERT INTO orders (orderid, Tafelnr) 
                VALUES (@orderId, @tafelNr);";

            using var connection = GetConnection();
            connection.Execute(sql, new { orderId, tafelNr });
        }

        /// <summary>
        /// deletes a single product from an order in the db
        /// </summary>
        /// <param name="productId">the product id</param>
        /// <param name="orderId">the order id</param>
        /// <returns></returns>
        public void DeleteSingle(int productId, string orderId)
        {
            //the query
            string sql = @"DELETE FROM orderentrys WHERE ProductId = @productId && @OrderId = @orderId LIMIT 1";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new { productId, orderId });
        }
        /// <summary>
        /// selete all order entrys with a specific product id
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteAllSpecific(int productId)
        {
            //the query
            string sql = @"DELETE FROM orderentrys WHERE ProductId = @productId";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new { productId});
        }


        public int PaySpecific(string orderId, int prodid, int numberOfItems)
        {
            using var connection = GetConnection();
            var sql = "update orderentrys set ispaid = 1 where OrderId = @orderId && ispaid = 0 && productid = @prodid LIMIT @numberOfItems";
            var removeSeparate = connection.Execute(sql, new { orderId, numberOfItems, prodid });
            return removeSeparate;
        }


        /// <summary>
        /// sets all entrys from a specific order to paid
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int PayAll(string orderId)
        {
            using var connection = GetConnection();
            var sql = "update orderentrys set ispaid = 1 where OrderId = @orderId";
            var removeSeparate = connection.Execute(sql, new { orderId });
            return removeSeparate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public IEnumerable<CafeCount> GetOrder(string orderid)
        {
            var sql = @"select
                Price,
                product.ProductId as ProductId,
                product.name as ProductName, 
                count(orderentrys.productid) as ProductCount,
                sum(if (orderentrys.ispaid, 1, 0)) as ProductCountPaid,
                sum(if (orderentrys.ispaid, 0, 1)) as ProductCountNotPaid,
                sum(if (orderentrys.ispaid, 0, product.Price)) as totalpay
                from orderentrys, product
                where orderentrys.productid = product.productid AND orderentrys.orderid = @orderid
                group by product.name ";


            using var connection = GetConnection();
            var order = connection.Query<CafeCount>(sql, new { orderid });
            return order;
        }
    }
}
