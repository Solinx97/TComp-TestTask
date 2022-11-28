﻿namespace TestApplication.DAL.Interfaces;

public interface IGenericRepository<TModel, TIdType>
    where TModel : class
    where TIdType : notnull
{
    Task<TModel> CreateAsync(TModel item);

    Task<int> UpdateAsync(TModel item);

    Task<int> DeleteAsync(TModel item);

    Task<TModel> GetByIdAsync(TIdType id);

    Task<IEnumerable<TModel>> GetAllAsync();
}
