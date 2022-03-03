namespace PizzaStore.Models;

public class Model
{
    public record Product(int Id, string Name, string Description, decimal Value);

    public record CreateProduct(string Name, string Description, decimal Value);
}