namespace BackEndTry2.Models
{
    public class Order
    {
        public long OrderID { get; set; }
        public string? Name { get; set; }  
        public long? CatID { get; set; }
        public long? DogID { get; set; }
        public Cat? Cat { get; set; }
        public Dog? Dog { get; set; }
    }
}
