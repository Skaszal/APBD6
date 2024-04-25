using System.Data.SqlClient;
using APBD6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD6.Controllers;

[ApiController]
[Route("controller-animals")]
public class StudentsController : ControllerBase
{

    private readonly IConfiguration _configuration;

    public StudentsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAllAnimals()
    {
        var response = new List<GetAnimalsResponse>();
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand("SELECT * FROM Animal", sqlConnection);
            sqlCommand.Connection.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                response.Add(new GetAnimalsResponse(
                        reader.GetInt32(0), 
                        reader.GetString(1), 
                        reader.GetString(2), 
                        reader.GetString(3), 
                        reader.GetString(4)
                    )
                );
            }
        }
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetAnimal(int id)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        var sqlCommand = new SqlCommand("SELECT * FROM Animal WHERE ID = @1", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@1", id);
        sqlCommand.Connection.Open();

        var reader = sqlCommand.ExecuteReader();
        if (!reader.Read()) return NotFound();

        return Ok(new GetAnimalDetailsResponse(
                reader.GetInt32(0), 
                reader.GetString(1),
                reader.GetString(2), 
                reader.GetString(3), 
                reader.GetString(4)
            )
        );
    }
    
    [HttpPost]
    public IActionResult CreateAnimal(CreateAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(
                "INSERT INTO Animal (Name, Description, Category, Asrea) values (@1, @2, @3, @4); SELECT CAST(SCOPE_IDENTITY() as int)",
                sqlConnection
            );
            sqlCommand.Parameters.AddWithValue("@1", request.Name);
            sqlCommand.Parameters.AddWithValue("@2", request.Description);
            sqlCommand.Parameters.AddWithValue("@3", request.Category);
            sqlCommand.Parameters.AddWithValue("@4", request.Area);
            sqlCommand.Connection.Open();
            
            var id = sqlCommand.ExecuteScalar();
            
            return Created($"students/{id}", new CreateAnimalResponse((int)id, request));
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult ReplaceAnimal(int id, ReplaceAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(
                "UPDATE Animal SET Name = @1, Descrition = @2, Category = @3, Area = @4 WHERE ID = @5",
                sqlConnection
            );
            sqlCommand.Parameters.AddWithValue("@1", request.Name);
            sqlCommand.Parameters.AddWithValue("@2", request.Description);
            sqlCommand.Parameters.AddWithValue("@3", request.Category);
            sqlCommand.Parameters.AddWithValue("@4", request.Area);
            sqlCommand.Parameters.AddWithValue("@5", id);
            sqlCommand.Connection.Open();

            var affectedRows = sqlCommand.ExecuteNonQuery();
            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult RemoveAnimal(int id)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = new SqlCommand("DELETE FROM Animal WHERE ID = @1", sqlConnection);
            command.Parameters.AddWithValue("@1", id);
            command.Connection.Open();

            var affectedRows = command.ExecuteNonQuery();

            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
}