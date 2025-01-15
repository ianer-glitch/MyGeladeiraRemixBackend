
using Grpc.Core;
using Identity.Domain.Protos;
using Microsoft.AspNetCore.Identity;

namespace User.Application.UseCases;

public class UserUseCase(UserManager<Domain.Models.User> userManager)
    : Identity.Domain.Protos.UserUseCase.UserUseCaseBase
{
    private readonly UserManager<Domain.Models.User> _userManager = userManager;

    public override async Task<PLoginOut> Login(PLoginIn request, ServerCallContext context)
    {
        try
        {
            // var user = await _userManager.FindByEmailAsync(request.Email);
            // ArgumentNullException.ThrowIfNull(user);    
            //
            throw new NotImplementedException();
            
        }
        catch (Exception e)
        {
            throw new NotImplementedException();
            throw;
        }   
    }
}