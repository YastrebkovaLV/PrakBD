using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
namespace Database_Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выбери действие \n1) Все записи\r\n2) Новый пользователь\r\n3) Обновить \r\n4) Удалить\r\n5) Авторизоваться");
                int a = Convert.ToInt32(Console.ReadLine());

                string connectionString = "Server = sql.bsite.net\\MSSQL2016; Database = lilechka_; User Id = lilechka_; Password = 20060701;TrustServerCertificate=true";
                string sqlExpression = "SELECT * FROM usersCon";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    if (a == 1)
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            string columnName0 = reader.GetName(0);
                            string columnName1 = reader.GetName(1);
                            string columnName2 = reader.GetName(2);
                            string columnName3 = reader.GetName(3);
                            string columnName4 = reader.GetName(4);

                            Console.WriteLine($"{columnName0} \t{columnName1} \t \t{columnName2} \t\t\t\t{columnName3}    {columnName4}");

                            // Console.WriteLine("{0,0}{1,10}{2,10}{3,20}{4,30}", columnName1, columnName2, columnName3, columnName4, columnName5);

                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(0);
                                string course_id = reader.GetString(1);
                                object name = reader[2];
                                string description = reader.GetString(3);
                                object video_url = reader[4];

                                //course_id = course_id.Length > 15 ? course_id.Substring(0, Math.Min(course_id.Length, 15)) + "..." : course_id;
                                //description = description.Length > 15 ? description.Substring(0, Math.Min(description.Length, 15)) + "..." : description;


                                Console.WriteLine($"{id} \t{course_id} \t{name} {description} {video_url}");
                                //Console.WriteLine("{0,1}{2,10}{2,10}{3,20}{4,30}", id, course_id, name, description, video_url);

                            }


                        }
                        await reader.CloseAsync();
                    }
                    if (a == 2)
                    {
                        Console.WriteLine("Имя: ");
                        string name = (Console.ReadLine());
                        Console.WriteLine("Возраст: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Логин: ");
                        string username = (Console.ReadLine());
                        Console.WriteLine("Пароль: ");
                        string password = (Console.ReadLine());
                        string addUsers = $"INSERT INTO usersCon (firstname, age, username, password) VALUES ('{name}', {age}, '{username}', '{password}')";
                        SqlCommand command = new SqlCommand(addUsers, connection);
                        await command.ExecuteNonQueryAsync();

                    }
                    if (a == 3)
                    {
                        Console.WriteLine("Введи ID пользователя, у которого нужно обновить данные");
                        string Up = (Console.ReadLine());
                        Console.WriteLine("Что изменить? 1. Имя 2. Логин 3. Возраст");
                        int Choose = Convert.ToInt32(Console.ReadLine());
                        if (Choose == 1)
                        {
                            Console.WriteLine("Новое имя: ");
                            string name = (Console.ReadLine());
                            string updateUsers = $"UPDATE usersCon SET firstname='{name}' WHERE Id='{Up}'";

                            SqlCommand command = new SqlCommand(updateUsers, connection);
                            int number = await command.ExecuteNonQueryAsync();
                            Console.WriteLine($"Обновлено объектов: {number}");
                        }

                        if (Choose == 2)
                        {
                            Console.WriteLine("Логин: ");
                            string username = (Console.ReadLine());
                            string updateUsers = $"UPDATE usersCon SET username='{username}' WHERE Id='{Up}'";

                            SqlCommand command = new SqlCommand(updateUsers, connection);
                            int number = await command.ExecuteNonQueryAsync();
                            Console.WriteLine($"Обновлено объектов: {number}");
                        }
                        if (Choose == 3)
                        {

                            Console.WriteLine("Новый возраст:");
                            int age = Convert.ToInt32(Console.ReadLine());
                            string updateUsers = $"UPDATE usersCon SET age='{age}' WHERE Id='{Up}'";

                            SqlCommand command = new SqlCommand(updateUsers, connection);
                            int number = await command.ExecuteNonQueryAsync();
                            Console.WriteLine($"Обновлено объектов: {number}");
                        }
                    }
             
                    if (a == 4)
                    {
                        Console.WriteLine("Введи ID пользователя для удаления");
                        int id = Convert.ToInt32(Console.ReadLine());
                        string Delete = $"DELETE  FROM usersCon WHERE Id='{id}'";

                        SqlCommand command = new SqlCommand(Delete, connection);
                        int number = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Удалено объектов: {number}");
                    }
                    if (a == 5)
                    {

                    }
                }
            }
            Console.Read();
        }
    }
}
