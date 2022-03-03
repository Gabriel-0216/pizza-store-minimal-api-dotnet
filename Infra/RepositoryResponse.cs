namespace PizzaStore.Infra;

public class RepositoryResponse
{
    public bool Success { get; set; }
    public List<Error> Errors { get; set; } = new List<Error>();

    public void SetError(string error)
    {
        Success = false;
        Errors.Add(new Error(error));
    }

    public void SetSuccess()
    {
        Success = true;
    }
    public record Error(string Description);

    private record ErrorImpl(string Description) : Error(Description);
}