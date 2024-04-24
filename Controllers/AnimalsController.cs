using APBD6.Models;
using APBD6.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD6.Controllers;

[ApiController]
[Route("animals")]
public class AnimalsController : ControllerBase
{

    private IMockDb _mockDb;

    public AnimalsController(IMockDb mockDb)
    {
        _mockDb = mockDb;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_mockDb.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetByID(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Add(Animal animal)
    {   
        //Sprawdzenie czy ID nie jest zajete => 409 (Conflict)
        if (_mockDb.GetAll().FirstOrDefault(e => e.ID == animal.ID) is not null)
        {
            return Conflict();
        }
        
        _mockDb.Add(animal);
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Replace(Animal animal, int id)
    {
        _mockDb.Replace(animal, id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _mockDb.Delete(id);
        return NoContent();
    }
}