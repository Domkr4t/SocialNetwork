using SocialNetwork.Domain.Response;
using SocialNetwork.Domain.ViewModels;

namespace SocialNetwork.Service.Interfaces
{
    public interface IAuthService
    {
        Task<IBaseResponse<UserViewModel>> AuthentificateUser(AuthViewModel model);
        Task<IBaseResponse<UserViewModel>> RegistrateUser(RegistrationViewModel model);
    }
}
