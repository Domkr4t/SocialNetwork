using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.Domain.Entity;
using SocialNetwork.Domain.Enum;
using SocialNetwork.Domain.Response;
using SocialNetwork.Domain.ViewModels;
using SocialNetwork.Service.Interfaces;

namespace SocialNetwork.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IBaseRepository<MessageEntity> _messageRepository;

        public AuthService(IBaseRepository<UserEntity> userRepository, IBaseRepository<MessageEntity> messageRepository) =>
            (_userRepository, _messageRepository) = (userRepository, messageRepository);

        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="model">AuthViewModel model</param>
        /// <returns>Returns a message about the success of user authentication</returns>
        public async Task<IBaseResponse<UserViewModel>> AuthentificateUser(AuthViewModel model)
        {
            try
            {
                var userEntity = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Login.Equals(model.Login) && x.Password.Equals(model.Password));

                if (userEntity == null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = $"Неправильный логин или пароль.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var user = new UserViewModel
                {
                    Id = userEntity.Id,
                    Surname = userEntity.Surname,
                    Name = userEntity.Name,
                    Middlename = userEntity.Middlename,
                };

                return new BaseResponse<UserViewModel>()
                {
                    Data = user,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="model">RegistrationViewModel model</param>
        /// <returns>Returns a message indicating that the user was successfully created</returns>
        public async Task<IBaseResponse<UserViewModel>> RegistrateUser(RegistrationViewModel model)
        {
            try
            {
                var userEntity = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Login.Equals(model.Login));

                if (userEntity != null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = $"Пользователь с таким логином уже существует.",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }

                userEntity = new UserEntity
                {
                    Login = model.Login,
                    Password = model.Password,
                    Surname = model.Surname,
                    Name = model.Name,
                    Middlename = model.Middlename,
                    ReceivedMessages = new List<MessageEntity>(),
                    SentMessages = new List<MessageEntity>()
                };

                await _userRepository.Create(userEntity);

                var user = new UserViewModel
                {
                    Id = userEntity.Id,
                    Surname = userEntity.Surname,
                    Name = userEntity.Name,
                    Middlename = userEntity.Middlename,
                };

                return new BaseResponse<UserViewModel>()
                {
                    Description = $"Пользователь {model.Login} создан.",
                    Data = user,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}