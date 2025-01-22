using Microsoft.AspNetCore.Identity;
using User.Application.UseCases;

namespace User.Service.Services;

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
    
    public override async Task<PCreateUserOut> CreateUser(PCreateUserIn request, ServerCallContext context)
    {
        try
        {
            var success =  await _userUseCase.CreateUserAsync(request);
            return new PCreateUserOut()
            {
                Success = success
            };
            

        }
        catch (Exception e)
        {
            throw new NotImplementedException();
            throw;
        }   
    }

    
}