using APBD6.Models;

namespace APBD6.Services;


public class MockDb : IMockDb
{
    private ICollection<Animal> _animals;

    public MockDb()
    {
        _animals = new List<Animal>()
        {
            new Animal()
            {
                ID = 0,
                Name = "Frida",
                Category = "Cat",
                Weight = 5,
                Color = "Tortoiseshell"
            },
            new Animal()
            {
                ID = 1,
                Name = "Salaterka",
                Category = "Cat",
                Weight = 6,
                Color = "Tabby"
            },
            new Animal()
            {
                ID = 2,
                Name = "Frodo",
                Category = "Dog",
                Weight = 15,
                Color = "Black"
            }
        };
        
    }
    
    public ICollection<Animal> GetAll()
    {
        return _animals;
    }

    public bool Add(Animal animal)
    {
        _animals.Add(animal);
        return true;
    }

    public bool Replace(Animal animal, int id)
    {
        var animalToRemove = _animals.FirstOrDefault(e => e.ID == id);
        if (animalToRemove is null)
        {
            return false;
        }

        _animals.Remove(animalToRemove);
        _animals.Add(animal);
        return true;
    }
    public bool Delete( int id)
    {
        var animalToRemove = _animals.FirstOrDefault(e => e.ID == id);
        if (animalToRemove is null)
        {
            return false;
        }
        _animals.Remove(animalToRemove);
        
        return true;
    }
}