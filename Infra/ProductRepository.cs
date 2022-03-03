using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Infra;

public class ProductRepository : IRepository<Model.Product>
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RepositoryResponse> Add(Model.Product entity)
    {
        var response = new RepositoryResponse();
        try
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            response.SetSuccess();
            return response;
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
            return response;
        }
        
    }

    public async Task<RepositoryResponse> Remove(int id)
    {
        var response = new RepositoryResponse();
        try
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                response.SetError("Product don't exists");
                return response;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            response.SetSuccess();
            return response;
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
            return response;
        }
    }

    public async Task<RepositoryResponse> Update(Model.Product entity)
    {
        var response = new RepositoryResponse();
        try
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            response.SetSuccess();
            return response;

        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
            return response;
        }
    }

    public async Task<Model.Product?> Get(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Model.Product>> Get(int skip, int take)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderBy(p=> p.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}