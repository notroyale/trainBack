using Microsoft.AspNetCore.Authorization;

namespace Auth;

public class IsAllowedToLoginHandler
{
    /*private readonly IUserService _userService;

    public IsAllowedToLoginHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAllowedToLoginRequirement requirement)
    {
        var bNumber = context.User.Identity?.Name;

        if (bNumber == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        IUserModel user = _userService.CreateUser(bNumber, requirement.AccessRight);

        if (user == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }*/
}