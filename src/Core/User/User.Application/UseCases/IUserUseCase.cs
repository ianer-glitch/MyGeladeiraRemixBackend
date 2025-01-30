using Identity.Domain.Protos;

namespace User.Application.UseCases;

public interface IUserUseCase
{
  public Task<PIsUserPasswordValidOut> IsUserPasswordValidAsync(PIsUserPasswordValidIn request);
  public Task<bool> CreateUserAsync(PCreateUserIn req);

  public Task<bool> DeleteUserAsync(PDeleteUserIn request);

  public Task<PGetUserRolesOut> GetUserRoles(PGetUserRolesIn request);
  public Task<bool> AddUserRoleAdministrator(string email);

  public  Task CreateRolesAsync();
}