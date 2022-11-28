using AutoMapper;
using TestApplication.BL.DTO;
using TestApplication.BL.Interfaces;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Interfaces;

namespace TestApplication.BL.Services;

internal class TableCService : IService<TableCDTO, int>
{
    private readonly IGenericRepository<TableC, int> _repository;
    private readonly IMapper _mapper;

    public TableCService(IGenericRepository<TableC, int> userRepository, IMapper mapper)
    {
        _repository = userRepository;
        _mapper = mapper;
    }

    public Task<TableCDTO> CreateAsync(TableCDTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableCDTO), $"The {nameof(TableCDTO)} can't be null");
        }

        return CreateInternalAsync(item);
    }

    public Task<int> DeleteAsync(TableCDTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableCDTO), $"The {nameof(TableCDTO)} can't be null");
        }

        return DeleteInternalAsync(item);
    }

    public async Task<IEnumerable<TableCDTO>> GetAllAsync()
    {
        var allData = await _repository.GetAllAsync();
        var result = _mapper.Map<List<TableCDTO>>(allData);

        return result;
    }

    public async Task<TableCDTO> GetByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        var resultMap = _mapper.Map<TableCDTO>(result);

        return resultMap;
    }

    public Task<int> UpdateAsync(TableCDTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableCDTO), $"The {nameof(TableCDTO)} can't be null");
        }

        return UpdateInternalAsync(item);
    }

    private async Task<TableCDTO> CreateInternalAsync(TableCDTO item)
    {
        var map = _mapper.Map<TableC>(item);
        var createdItem = await _repository.CreateAsync(map);
        var resultMap = _mapper.Map<TableCDTO>(createdItem);

        return resultMap;
    }

    private async Task<int> DeleteInternalAsync(TableCDTO item)
    {
        var map = _mapper.Map<TableC>(item);
        var rowsAffected = await _repository.DeleteAsync(map);

        return rowsAffected;
    }

    private async Task<int> UpdateInternalAsync(TableCDTO item)
    {
        var map = _mapper.Map<TableC>(item);
        var rowsAffected = await _repository.UpdateAsync(map);

        return rowsAffected;
    }
}
