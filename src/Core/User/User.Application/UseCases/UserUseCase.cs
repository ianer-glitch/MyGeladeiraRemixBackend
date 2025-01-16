
using Grpc.Core;
using Identity.Domain.Protos;
using Microsoft.AspNetCore.Identity;

namespace User.Application.UseCases;

public class UserUseCase(UserManager<Domain.Models.User> userManager) : IUserUseCase
    
{
    private readonly UserManager<Domain.Models.User> _userManager = userManager;

    public async Task<bool> IsUserPasswordValidAsync(PLoginIn request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            ArgumentNullException.ThrowIfNull(user);    
            return await _userManager.CheckPasswordAsync(user, request.Password);    
            
        }
        catch (Exception e)
        {
            throw new NotImplementedException();
            throw;
        }   
    }
}