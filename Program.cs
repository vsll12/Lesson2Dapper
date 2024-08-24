using Dapper;
using Lesson2Dapper.Models;
using Lesson2Dapper.Models.DTOs;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


var conStr = "Data Source=DESKTOP-7F74UDB; Initial Catalog=Library;Integrated Security=true";

using var sqlConnection = new SqlConnection(conStr);

//Querying Multiple Rows from View using DTO


var query = "SELECT * FROM BooksStudentsAndTeacherTook";

var result = sqlConnection.Query<BookDTO>(query);

foreach (var item in result)
{
    Console.WriteLine(item.BookName);
}

Console.WriteLine();


//Get All Category With Books

var query2 = "SELECT * FROM Categories JOIN Books ON Categories.Id = Books.Id_Category ";

var books = sqlConnection.Query<Category, Book, Book>(query2, map: (category, book) =>
{
    book.Category = category;
    return book;
},
splitOn: "Id").ToList();

foreach (var item in books)
{
    Console.WriteLine($"{item.Name}  {item.Category.Name}");
}


//Get All Books With Theme

var query3 = "SELECT * FROM Themes JOIN Books ON Themes.Id = Books.Id_Themes";

var books2 = sqlConnection.Query<Theme, Book, Book>(query3, map: (theme, book) =>
{
    book.Theme = theme;
    return book;
},
splitOn: "Id");

foreach (var item in books2)
{
    Console.WriteLine($"{item.Name}    {item.Theme.Name}");
}


//Exec Procedure

var procedure = "sp_get_lazy_students_with_count";

var parameters = new DynamicParameters();

parameters.Add("Count", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

var result2 = sqlConnection.Query(procedure,parameters,commandType: CommandType.StoredProcedure);

var count = parameters.Get<int>("Count");

Console.WriteLine(count);