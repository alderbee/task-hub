using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TaskPulse.Domain.interfaces.Helper;
using TaskPulse.Domain.Options;

namespace TaskPulse.Application.Helper;

public class CaptchaVerificationService : ICaptchaVerificationService
{
    private CaptchaOptions captchaSettings;

    private string ClientKey => captchaSettings.ClientKey;

    public CaptchaVerificationService(IOptions<CaptchaOptions> captchaSettings)
    {
        this.captchaSettings = captchaSettings.Value;
    }

    public async Task<bool> IsCaptchaValid(string token)
    {
        var result = false;

        using var client = new HttpClient();

        var response =
            await client.PostAsync($"{captchaSettings.VerificationUrl}?secret={captchaSettings.ServerKey}&response={token}",
                null);
        if ((response.IsSuccessStatusCode))
        {
            result = true;
        }
        else
        {
            return false;
        }

        return result;
    }
}