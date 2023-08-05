namespace AspReactTestApp.Dto
{
    public class GetCarsRequestDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
    }
}
