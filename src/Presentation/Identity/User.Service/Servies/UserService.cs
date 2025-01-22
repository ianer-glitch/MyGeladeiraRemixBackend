using Microsoft.AspNetCore.Identity;
using User.Application.UseCases;

namespace User.Service.Servies;

public class UserService(IUserUseCase userUseCase) : Identity.Domain.Protos.UserService.UserServiceBase
{
    private readonly IUserUseCase _userUseCase = userUseCase; 
    public override async Task<PIsUserPasswordValidOut> IsUserPasswordValid(PIsUserPasswordValidIn request, ServerCallContext context)
    {
        try
        {
            var sucess =  await _userUseCase.IsUserPasswordValidAsync(request);
            return new PIsUserPasswordValidOut()
            {
                IsValid = sucess
            };
            

        }
        catch (Exception e)
        {
            throw new NotImplementedException();
            throw;
        }   
    }

    
}