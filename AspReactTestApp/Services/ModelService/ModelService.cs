using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;

namespace AspReactTestApp.Services.ModelService
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public ModelService(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddModel(Model model)
        {
            ResponseDto responseDto = new();
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

        public async Task<ResponseDto> RemoveModelById(int id)
        {
            ResponseDto responseDto = new();
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

        public async Task<List<GenericEntityDto>> GetModelsByBrandId(int brandId)
        {
            try
            {
                var modelList = await _modelRepository.GetList(m => m.BrandId == brandId);
                List<GenericEntityDto> modelsDtos = _mapper.Map<List<Model>, List<GenericEntityDto>>(modelList);
                return modelsDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
