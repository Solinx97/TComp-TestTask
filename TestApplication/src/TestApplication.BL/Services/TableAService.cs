using AutoMapper;
using TestApplication.BL.DTO;
using TestApplication.BL.Interfaces;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Interfaces;

namespace TestApplication.BL.Services;

public class TableAService : IService<TableADTO, int>
{
    private readonly IGenericRepository<TableA, int> _repository;
    private readonly IMapper _mapper;

    public TableAService(IGenericRepository<TableA, int> userRepository, IMapper mapper)
    {
        _repository = userRepository;
        _mapper = mapper;
    }

    public Task<TableADTO> CreateAsync(TableADTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableADTO), $"The {nameof(TableADTO)} can't be null");
        }

        return CreateInternalAsync(item);
    }

    public Task<int> DeleteAsync(TableADTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableADTO), $"The {nameof(TableADTO)} can't be null");
        }

        return DeleteInternalAsync(item);
    }

    public async Task<IEnumerable<TableADTO>> GetAllAsync()
    {
        var allData = await _repository.GetAllAsync();
        var result = _mapper.Map<List<TableADTO>>(allData);

        return result;
    }

    public async Task<TableADTO> GetByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        var resultMap = _mapper.Map<TableADTO>(result);

        return resultMap;
    }

    public Task<int> UpdateAsync(TableADTO item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(TableADTO), $"The {nameof(TableADTO)} can't be null");
        }

        return UpdateInternalAsync(item);
    }

    private async Task<TableADTO> CreateInternalAsync(TableADTO item)
    {
        var map = _mapper.Map<TableA>(item);
        var createdItem = await _repository.CreateAsync(map);
        var resultMap = _mapper.Map<TableADTO>(createdItem);

        return resultMap;
    }

    private async Task<int> DeleteInternalAsync(TableADTO item)
    {
        var map = _mapper.Map<TableA>(item);
        var rowsAffected = await _repository.DeleteAsync(map);

        return rowsAffected;
    }

    private async Task<int> UpdateInternalAsync(TableADTO item)
    {
        var map = _mapper.Map<TableA>(item);
        var rowsAffected = await _repository.UpdateAsync(map);

        return rowsAffected;
    }
}
