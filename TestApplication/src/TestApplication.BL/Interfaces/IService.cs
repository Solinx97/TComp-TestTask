namespace TestApplication.BL.Interfaces;

public interface IService<TModel, TIdType>
    where TModel : class
    where TIdType : notnull
{
    Task<TModel> CreateAsync(TModel item);

    Task<int> UpdateAsync(TModel item);

    Task<int> DeleteAsync(TModel item);

    Task<IEnumerable<TModel>> GetAllAsync();

    Task<TModel> GetByIdAsync(TIdType id);
}
