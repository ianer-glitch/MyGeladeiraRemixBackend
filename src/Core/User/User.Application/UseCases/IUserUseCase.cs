using Identity.Domain.Protos;

namespace User.Application.UseCases;

public interface IUserUseCase
{
  public Task<bool> IsUserPasswordValidAsync(PIsUserPasswordValidIn request);
  public Task<bool> CreateUserAsync(PCreateUserIn req);

  public Task<bool> DeleteUserAsync(PDeleteUserIn request);
}