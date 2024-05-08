using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.Domain.Entity;
using SocialNetwork.Domain.Enum;
using SocialNetwork.Domain.Extensions;
using SocialNetwork.Domain.Filters;
using SocialNetwork.Domain.Response;
using SocialNetwork.Domain.ViewModels;
using SocialNetwork.Service.Interfaces;

namespace SocialNetwork.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IBaseRepository<MessageEntity> _messageRepository;

        public UserService(IBaseRepository<UserEntity> userRepository, IBaseRepository<MessageEntity> messageRepository) =>
            (_userRepository, _messageRepository) = (userRepository, messageRepository);

        /// <summary>
        /// Method to get the list of received messages
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Inbox list received status message</returns>
        public async Task<IBaseResponse<IEnumerable<MessageViewModel>>> GetAllReceivedMessages(MessageFilter filter)
        {
            try
            {
                var messages = await _messageRepository.GetAll()
                    .Where(x => x.ToUserId == filter.UserId)
                    .WhereIf(!string.IsNullOrEmpty(filter.Login),
                        x => x.FromUser.Login.Contains(filter.Login))
                    .WhereIf(!string.IsNullOrEmpty(filter.From),
                        x => x.DateOfMessage.Date >= DateTime.Parse(filter.From))
                    .WhereIf(!string.IsNullOrEmpty(filter.To),
                        x => x.DateOfMessage.Date <= DateTime.Parse(filter.To))
                    .WhereIf(filter.Status != MessageStatus.All,
                        x => Convert.ToInt32(x.IsReading) == (int)filter.Status)
                    .Select(x => new MessageViewModel
                    {
                        Id = x.Id,
                        FromUserLogin = x.FromUser.Login,
                        Header = x.Header,
                        IsReading = x.IsReading,
                        DateOfMessage = x.DateOfMessage.ToString("dd.MM.yyyy"),
                    })
                    .ToListAsync();

                return new BaseResponse<IEnumerable<MessageViewModel>>
                {
                    Data = messages,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<IEnumerable<MessageViewModel>>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Receiving information about the message
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>Message with the status of receiving information about the message</returns>
        public async Task<IBaseResponse<OneMessageViewModel>> GetOneMessage(int messageId)
        {
            try
            {
                var messageEntity = await _messageRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == messageId);

                if (messageEntity == null)
                {
                    return new BaseResponse<OneMessageViewModel>
                    {
                        Description = "Письмо не найдено.",
                        StatusCode = StatusCode.MessageNotFound
                    };
                }

                var message = new OneMessageViewModel
                {
                    Id = messageEntity.Id,
                    Header = messageEntity.Header,
                    Body = messageEntity.Body,
                    IsReading = messageEntity.IsReading,
                    DateOfMessage = messageEntity.DateOfMessage.ToString("dd.MM.yyyy"),
                };

                return new BaseResponse<OneMessageViewModel>
                {
                    Data = message,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<OneMessageViewModel>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Retrieving user information
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Message with the status of receiving user information </returns>
        public async Task<IBaseResponse<UserViewModel>> GetUserAccountInformation(int userId)
        {
            try
            {
                var userEntity = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (userEntity == null)
                {
                    return new BaseResponse<UserViewModel>
                    {
                        Description = "Пользователь не найден.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var user = new UserViewModel
                {
                    Id = userEntity.Id,
                    Login = userEntity.Login,
                    Surname = userEntity.Surname,
                    Name = userEntity.Name,
                    Middlename = userEntity.Middlename,
                };

                return new BaseResponse<UserViewModel>
                {
                    Data = user,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<UserViewModel>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Method for sending messages
        /// </summary>
        /// <param name="model">SendMessageViewModel model</param>
        /// <returns>Message with message sending status</returns>
        public async Task<IBaseResponse<SendMessageViewModel>> SendMessage(SendMessageViewModel model)
        {
            try
            {
                var userFrom = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.FromUserId);

                if (userFrom == null)
                {
                    return new BaseResponse<SendMessageViewModel>
                    {
                        Description = "Отправитель не найден.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                await _userRepository.Attach(userFrom);

                var userTo = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Login.Equals(model.Login));

                if (userTo == null)
                {
                    return new BaseResponse<SendMessageViewModel>
                    {
                        Description = $"Пользователь {model.Login} не найден.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                await _userRepository.Attach(userTo);

                var message = new MessageEntity
                {
                    FromUserId = userFrom.Id,
                    FromUser = userFrom,
                    ToUserId = userTo.Id,
                    ToUser = userTo,
                    Header = model.Header,
                    Body = model.Body,
                    IsReading = false,
                    DateOfMessage = DateTime.Now,
                };

                await _messageRepository.Create(message);

                return new BaseResponse<SendMessageViewModel>
                {
                    Description = "Письмо отправлено.",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<SendMessageViewModel>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Method for changing the status of a message to “read”
        /// </summary>
        /// <param name="messageId">messageId</param>
        /// <returns>Message status change message</returns>
        public async Task<IBaseResponse<MessageViewModel>> SetIsReadMessage(int messageId)
        {
            try
            {
                var message = await _messageRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == messageId);

                if (message == null)
                {
                    return new BaseResponse<MessageViewModel>
                    {
                        Description = $"Письмо не найдено.",
                        StatusCode = StatusCode.MessageNotFound
                    };
                }

                message.IsReading = true;

                await _messageRepository.Update(message);

                return new BaseResponse<MessageViewModel>
                {
                    Description = $"Письмо {message.Header} прочитано.",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<MessageViewModel>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}