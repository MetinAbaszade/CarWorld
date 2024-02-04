using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FeatureService;

public class FeatureService(IFeatureRepository featureRepository) : IFeatureService
{
    private readonly IFeatureRepository _featureRepository = featureRepository;

    public async Task<ResponseDTO> AddFeature(Feature feature)
    {
        ResponseDTO responseDto = new();
        try
        {
            await _featureRepository.Add(feature);
            responseDto.IsSuccessfull = true;
            responseDto.Message = "Feature Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
        }
        return responseDto;
    }

    public async Task<ResponseDTO> RemoveFeatureById(int id)
    {
        ResponseDTO responseDto = new();
        try
        {
            var feature = await _featureRepository.Get(f => f.Id == id);
            if (feature != null)
            {
                await _featureRepository.Delete(feature);
                responseDto.Message = "Feature Not Found!";
                return responseDto;
            }
            responseDto.IsSuccessfull = true;
            responseDto.Message = "Feature Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
        }
        return responseDto;
    }

    public async Task<List<GenericEntityDTO>> GetAllFeatures(int languageId)
    {
        try
        {
            var featureDtos = await _featureRepository.Select(
                select: f => new GenericEntityDTO
                {
                    Id = f.Id,
                    Name = f.FeatureLocales.SingleOrDefault(fl => fl.LanguageId == languageId).Name
                });

           return featureDtos;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
