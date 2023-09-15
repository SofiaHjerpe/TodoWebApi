using System.Data.SqlClient;
using TodosWebApi.Models;

namespace TodosWebApi
{
    public class Dbconnection
    {

        public List<Todo> GetTodos()
        {
            List<Todo> todos = new List<Todo>(); 
             var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT * FROM Todo";
            var reader = cmd.ExecuteReader();   
            while (reader.Read()) {
                var todo = new Todo()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Date = DateTime.Parse(reader["Date"].ToString()),
                    Done = bool.Parse(reader["Done"].ToString())
                };
              todos.Add(todo);
             

            }
            return todos;
        }
        public Todo GetTodo(int id)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT * FROM Todo WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id);
            var reader = cmd.ExecuteReader();

            while (reader.Read()) {

                var todo = new Todo()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Date = DateTime.Parse(reader["Date"].ToString()),
                    Done = bool.Parse(reader["Done"].ToString())
                };
                return todo;
            }
            return null;
        }

        public void SaveTodo(Todo todo)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "INSERT INTO Todo(Title, Date, Done) VALUES (@title, @date, @done)";
            cmd.Parameters.AddWithValue("title", todo.Title);
            cmd.Parameters.AddWithValue("date", todo.Date);
            cmd.Parameters.AddWithValue("done", todo.Done);

            cmd.ExecuteNonQuery();

        }

        public void UpdateTodo(int id, Todo todo)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE Todo SET Title= @title, Date= @date, Done= @done WHERE Id = @id";
            cmd.Parameters.AddWithValue("title", todo.Title);
            cmd.Parameters.AddWithValue("date", todo.Date);
            cmd.Parameters.AddWithValue("done", todo.Done);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();  
        }
        public void DeleteTodo(int id) { 
           var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM Todo WHERE Id= @id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();  
        }
        private SqlCommand GetSqlCommand()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=TodosDB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();  
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;

        }
    }
}
