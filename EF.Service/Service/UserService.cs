using AutoMapper;
using EF.Repository.Interface;
using EF.Repository.Model;
using EF.Service.DTO;
using EF.Service.Interface;

namespace EF.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> DeleteAsync(int id)
        {
            try
            {
                if (await _userRepository.DeleteAsync(id))
                {
                    return new ResponseDTO()
                    {
                        Message = "Successfully"
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> GetAllAsync()
        {
            try
            {
                return new ResponseDTO()
                {
                    Data = _mapper.Map<List<UserInforDTO>>(await _userRepository.GetAllAsync()),
                    Message = "All user data"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> GetByIdAsync(int id)
        {
            try
            {

                var data = await _userRepository.GetByIdAsync(id);
                if (data != null)
                {
                    return new ResponseDTO()
                    {
                        Data = _mapper.Map<UserInforDTO>(await _userRepository.GetByIdAsync(id)),
                        Message = "User data"
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "No user found",
                        Status = 404
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> InsertAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                user.IsActive = true;
                user.CreatedOn = DateTime.UtcNow;
                if (await _userRepository.InsertAsync(user))
                {
                    return new ResponseDTO()
                    {
                        Data = _mapper.Map<UserInforDTO>(user),
                        Message = "User data"
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "Error during inserting user.",
                        Status = 400
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> UpdatetAsync(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userUpdateDTO.Id);
                if (user == null)
                {
                    return new ResponseDTO()
                    {
                        Message = "User not found"
                    };
                }
                user.Email = userUpdateDTO.Email;
                user.Password = userUpdateDTO.Password;
                user.Name = userUpdateDTO.Name;
                if (await _userRepository.UpdateAsync(user))
                {
                    return new ResponseDTO()
                    {
                        Message = "Successfully"
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
    }
}
