namespace EA.AdminPanel.Helper
{
    public class TokenHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = HttpContext.Current.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
