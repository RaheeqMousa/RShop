using RShop.DAL.Models;

namespace RShop.DAL.DTO.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string FullName { get; set; }
        public ApplicationUser User { get; set; }
    }
}