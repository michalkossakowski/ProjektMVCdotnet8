using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProjektMVCdotnet8.Areas.Identity.Data;

public class EntityUserManager<TUser> : UserManager<TUser> where TUser : class
{

    public EntityUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public string GetNick(TUser user)
    {
        return (user as UserEntity)?.UserName;
    }
}