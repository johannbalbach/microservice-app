﻿using System.Net;
using Microsoft.Extensions.Primitives;

namespace APIGateway.Models
{
    public class Router
    {
        public List<Route> Routes { get; set; }
        public Destination AuthenticationService { get; set; }

        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
            AuthenticationService = JsonLoader.Deserialize<Destination>(Convert.ToString(router.authenticationService));

        }

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            string basePath = '/' + path.Split('/')[1];

            Destination destination;
            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                return ConstructErrorMessage("The path could not be found.");
            }

            if (destination.RequiresAuthentication)
            {
                string token = request.Headers["token"];
                request.Query.Append(new KeyValuePair<string, StringValues>("token", new StringValues(token)));
                HttpResponseMessage authResponse = await AuthenticationService.SendRequest(request);
                if (!authResponse.IsSuccessStatusCode) return ConstructErrorMessage("Authentication failed.");
            }

            return await destination.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }

    }
}
