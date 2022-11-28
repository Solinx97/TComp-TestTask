using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Core;
using TestApplication.DAL.Interfaces;

namespace TestApplication.DAL.Repositories;

public class GenericRepository<TModel, TIdType> : IGenericRepository<TModel, TIdType>
    where TModel : class
    where TIdType : notnull
{
    private readonly MyTestApplicationContext _context;

    public GenericRepository(MyTestApplicationContext context)
    {
        _context = context;
    }

    public async Task<TModel> CreateAsync(TModel item)
    {
        var entityEntry = await _context.Set<TModel>().AddAsync(item);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<int> DeleteAsync(TModel item)
    {
        _context.Set<TModel>().Remove(item);
        var rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected;
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        return await _context.Set<TModel>().AsNoTracking().ToListAsync();
    }

    public async Task<TModel> GetByIdAsync(TIdType id)
    {
        var entity = await _context.Set<TModel>().FindAsync(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<int> UpdateAsync(TModel item)
    {
        _context.Entry(item).State = EntityState.Modified;
        var rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected;
    }
}
