namespace project.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }
        public ICollection<Product> Products { get; }

    }
}
