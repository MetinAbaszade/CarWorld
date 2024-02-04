namespace AspReactTestApp.DTO;

public class GetCarsRequestDTO
{
    public int? BrandId { get; set; }
    public int? ModelId { get; set; }
    public int? FuelTypeId { get; set; }
    public int? GearTypeId { get; set; }
    public int? CategoryId { get; set; }
    public int? MinYearId { get; set; }
    public int? MaxYearId { get; set; }
    public int ColorId { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Sort { get; set; }
    public int LanguageId { get; set; }
    public int? TransmissionId { get; set; }
}
