using System.Data;
using Dapper;
using nhl_stenden_cafe.Pages.Models;

namespace nhl_stenden_cafe.Pages.Repository
{
    public class CategoryRepository
    {
        /// <summary>
        /// connects to database. extracts from class DbUtils
        /// </summary>
        /// <returns></returns>
        private static IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        /// <summary>
        /// gets category by ID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category Get(int categoryId)
        {
            string sql = "SELECT * FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = GetConnection();
            //singlequery for one result
            var category = connection.QuerySingle<Category>(sql, new { categoryId });
            return category;
        }

        /// <summary>
        /// gets all categories and returns them in a IEnumerable list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> Get()
        {
            string sql = "SELECT * FROM Category ORDER BY Name";
            
            using var connection = GetConnection();
            //normal query for list
            var categories = connection.Query<Category>(sql);
            return categories;
        }

      /// <summary>
      /// add a category
      /// </summary>
      /// <param name="name">een category</param>
      /// <returns></returns>
        public int Add(string name)
        {
            string sql = @"
                INSERT INTO Category (Name) 
                VALUES (@Name);";
                
            using var connection = GetConnection();
            //queru with bool return
            int val = connection.Execute(sql, new { name });
            return val;
        }

        /// <summary>
        /// deletes a category with the given id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool Delete(int categoryId)
        {
            string sql = @"DELETE FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { categoryId });
            return numOfEffectedRows == 1;
        }

        /// <summary>
        /// updates a record of a category by its id
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Category Update(Category category)
        {
            string sql = @"
                UPDATE Category SET 
                    Name = @Name 
                WHERE CategoryId = @CategoryId;
                SELECT * FROM Category WHERE CategoryId = @CategoryId";
            
            using var connection = GetConnection();
            var updatedCategory = connection.QuerySingle<Category>(sql, category);
            return updatedCategory;
        }
    }
}