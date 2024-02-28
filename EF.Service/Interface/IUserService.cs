using EF.Repository.Model;
using EF.Service.DTO;

namespace EF.Service.Interface
{
    public interface IUserService
    {
        Task<ResponseDTO> GetAllAsync();
        Task<ResponseDTO> GetByIdAsync(int id);
        Task<ResponseDTO> InsertAsync(UserDTO userDTO);
        Task<ResponseDTO> UpdatetAsync(UserUpdateDTO userUpdateDTO);
        Task<ResponseDTO> DeleteAsync(int id);
    }
}
