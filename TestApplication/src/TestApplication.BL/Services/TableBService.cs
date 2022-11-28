using AutoMapper;
using TestApplication.BL.DTO;
using TestApplication.BL.Interfaces;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Interfaces;

namespace TestApplication.BL.Services;

internal class TableBService : IService<TableBDTO, int>
{
    private readonly IGenericRepository<TableB, int> _repository;
    private readonly IMapper _mapper;

    public TableBService(IGenericRepository<TableB, int> userRepository, IMapper mapper)
    {
        _repository = userRepository;
        _mapper = mapper;
    }

    public Task<TableBDTO> CreateAsync(TableBDTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableBDTO), $"The {nameof(TableBDTO)} can't be null");
        }

        return CreateInternalAsync(item);
    }

    public Task<int> DeleteAsync(TableBDTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableBDTO), $"The {nameof(TableBDTO)} can't be null");
        }

        return DeleteInternalAsync(item);
    }

    public async Task<IEnumerable<TableBDTO>> GetAllAsync()
    {
        var allData = await _repository.GetAllAsync();
        var result = _mapper.Map<List<TableBDTO>>(allData);

        return result;
    }

    public async Task<TableBDTO> GetByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        var resultMap = _mapper.Map<TableBDTO>(result);

        return resultMap;
    }

    public Task<int> UpdateAsync(TableBDTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableBDTO), $"The {nameof(TableBDTO)} can't be null");
        }

        return UpdateInternalAsync(item);
    }

    private async Task<TableBDTO> CreateInternalAsync(TableBDTO item)
    {
        var map = _mapper.Map<TableB>(item);
        var createdItem = await _repository.CreateAsync(map);
        var resultMap = _mapper.Map<TableBDTO>(createdItem);

        return resultMap;
    }

    private async Task<int> DeleteInternalAsync(TableBDTO item)
    {
        var map = _mapper.Map<TableB>(item);
        var rowsAffected = await _repository.DeleteAsync(map);

        return rowsAffected;
    }

    private async Task<int> UpdateInternalAsync(TableBDTO item)
    {
        var map = _mapper.Map<TableB>(item);
        var rowsAffected = await _repository.UpdateAsync(map);

        return rowsAffected;
    }
}