
using Grpc.Core;
using Identity.Domain.Protos;
using Microsoft.AspNetCore.Identity;


namespace User.Application.UseCases;

public class UserUseCase(UserManager<Domain.Models.User> userManager) : IUserUseCase
    
{
    private readonly UserManager<Domain.Models.User> _userManager = userManager;

    public async Task<bool> IsUserPasswordValidAsync(PIsUserPasswordValidIn request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return false;
            
            return await _userManager.CheckPasswordAsync(user, request.Password);    
            
        }
        catch (Exception e)
        {
            throw new NotImplementedException();
            throw;
        }   
    }

    public async Task<bool> CreateUserAsync(PCreateUserIn req)
    {
        try
        {

            var existingUser = await _userManager.FindByEmailAsync(req.Email);
            if (existingUser is not null)
                return false;

            var user = new Domain.Models.User(
                req.FirstName,
                req.LastName,
                req.BirthDate.ToDateTime(),
                req.Email);

            var success = await _userManager.CreateAsync(user, req.Password);

            return success == IdentityResult.Success;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}