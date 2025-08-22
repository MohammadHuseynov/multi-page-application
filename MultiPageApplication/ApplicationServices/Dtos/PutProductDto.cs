namespace MultiPageApplication.ApplicationServices.Dtos
{
    public class PutProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal UnitPrice { get; set; }
       
    }
}
