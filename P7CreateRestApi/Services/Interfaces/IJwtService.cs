using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenererTokenAsync(User user);
    }
}
