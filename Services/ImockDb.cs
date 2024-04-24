using APBD6.Models;

namespace APBD6.Services;

public interface IMockDb
{
    public ICollection<Animal> GetAll();
    public bool Add(Animal animal);
    public bool Replace(Animal animal, int id);
    public bool Delete(int id);
}