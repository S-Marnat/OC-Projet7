using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenererTokenAsync(User user);
    }
}
