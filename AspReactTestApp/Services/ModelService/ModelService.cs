using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.ModelService
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
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

        public async Task<List<Model>> GetModelsByBrandId(int brandId)
        {
            try
            {
                var modelList = await _modelRepository.GetList(m => m.BrandId == brandId);
                return modelList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
