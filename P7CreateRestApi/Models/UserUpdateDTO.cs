using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class UserUpdateDTO
    {
        public string? UserName { get; set; }
        public string? Fullname { get; set; }
    }
}
