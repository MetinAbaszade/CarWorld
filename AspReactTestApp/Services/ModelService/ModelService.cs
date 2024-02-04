using AutoMapper;
using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.ModelService;

public class ModelService(IModelRepository modelRepository, IMapper mapper) : IModelService
{
    private readonly IModelRepository _modelRepository = modelRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseDTO> AddModel(Model model)
    {
        ResponseDTO responseDto = new();
        try
        {
            await _modelRepository.Add(model);
            responseDto.IsSuccessfull = true;
            responseDto.Message = "Model Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
        }
        return responseDto;
    }

    public async Task<ResponseDTO> RemoveModelById(int id)
    {
        ResponseDTO responseDto = new();
        try
        {
            var model = await _modelRepository.Get(m => m.Id == id);
            if (model != null)
            {
                await _modelRepository.Delete(model);
                responseDto.Message = "Model Not Found!";
                return responseDto;
            }
            responseDto.IsSuccessfull = true;
            responseDto.Message = "Model Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
        }
        return responseDto;
    }

    public async Task<List<GenericEntityDTO>> GetModelsByBrandId(int brandId)
    {
        try
        {
            var modelList = await _modelRepository.GetList(m => m.BrandId == brandId);
            List<GenericEntityDTO> modelsDtos = _mapper.Map<List<Model>, List<GenericEntityDTO>>(modelList);
            return modelsDtos;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
