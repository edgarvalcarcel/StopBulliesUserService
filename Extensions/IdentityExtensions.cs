using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using StopBulliesUserService.Domain.Entities;
using System.Text;

namespace StopBulliesUserService.Extensions;

internal sealed class IdentityExtensions
{
    private readonly UserManager<User> _userManager;
    public IdentityExtensions(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> CreateEmailConfirmationCodeAsync(User user)
    {
        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string Encode = WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes(code));
        string confirmationCode = Encoding.ASCII.GetString(WebEncoders.Base64UrlDecode(Encode));
        return confirmationCode;
    }
}
