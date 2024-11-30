using TaskPulse.Domain.Entities;
using TaskPulse.Domain.interfaces;
using TaskPulse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Infrastructure.Services;
using AutoMapper;
using TaskPulse.Domain.Exceptions;
using TaskPulse.Domain.Helpers;


namespace TaskPulse.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context, ITokenCreator token, IEncryptPassword encryptPassword,IMapper mapper)
    : IUserRepository
{
    public async Task<LoginResponse> UserRegistration(UserRegistration userentity)
    {
        var data =  await context.UsersData.SingleOrDefaultAsync(user => user.username == userentity.username || user.email == userentity.email);
        
        if (data?.email is not null || data?.username is not null)
        {
            throw new GeneralException(Constants.ErrorMessages.UserIdExist);
        }
        UsersData user = mapper.Map<UsersData>(userentity);
        
        user.passwordHash = encryptPassword.Encrypt(user.passwordHash);
        context.UsersData.Add(user);
        await context.SaveChangesAsync(); 
        
        LoginResponse response = new LoginResponse();
        
        response.userId = user.userId;;
        response.token =await token.CreateToken(user?.username);
        return response;
    }

    public async Task<LoginResponse> LoginUser(LoginUser entity)
    {
        LoginResponse response = new LoginResponse();
        var user = await context.UsersData.SingleOrDefaultAsync(u => u.username == entity.username);
        if (user is null)
        {
            throw new GeneralException(Constants.ErrorMessages.UserIdNonExist);

        }
        
        var passwordHash = encryptPassword.Encrypt(entity.password);

        if (!(passwordHash.Equals(user.passwordHash,StringComparison.InvariantCultureIgnoreCase)))
        {
            throw new GeneralException(Constants.ErrorMessages.InvalidPassword);
        }
        
        response.userId = user.userId;
        response.token = await token.CreateToken(user.username);
        return response;
      
    }
}