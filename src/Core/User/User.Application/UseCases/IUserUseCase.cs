using Identity.Domain.Protos;

namespace User.Application.UseCases;

public interface IUserUseCase
{
  public Task<bool> IsUserPasswordValidAsync(PLoginIn request);
}