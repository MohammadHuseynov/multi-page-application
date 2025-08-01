namespace MultiPageApplication.ApplicationServices.Dtos
{
    public class PutProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
