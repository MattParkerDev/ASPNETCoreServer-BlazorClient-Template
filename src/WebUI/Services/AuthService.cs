namespace WebUI.Services;

public class AuthService
{
    private readonly TemplateAppClient _templateAppClient;

    public AuthService(TemplateAppClient templateAppClient)
    {
        _templateAppClient = templateAppClient;
    }

    public async Task<bool> CheckAuthAsync()
    {
        try
        {
            var response = await _templateAppClient.CheckAuthAsync();
            return true;
        }
        catch (ApiException e)
        {
            var statusCode = e.StatusCode;
            return false;
        }
    }
}
