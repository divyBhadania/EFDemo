using EF.Service.DTO;

namespace EF.Service.Interface
{
    public interface IAuthService
    {
        Task<ResponseDTO> GetTokenAsync(LoginDTO loginDTO);
    }
}
