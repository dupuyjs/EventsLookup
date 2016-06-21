using MeetupLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using MeetupLibrary.Helpers;
//using Windows.Web.Http.Filters;

namespace MeetupLibrary
{
    public class SimpleServiceClient : IDisposable
    {
        //private HttpBaseProtocolFilter filter;
        private HttpClient httpClient;
        CancellationTokenSource cts;

        private int xRateLimitRemaining = 20;

        public SimpleServiceClient()
        {
            //filter = new HttpBaseProtocolFilter();
            httpClient = new HttpClient();
            cts = new CancellationTokenSource();
        }

        public void Cancel()
        {
            cts.Cancel();
            cts.Dispose();

            // Re-create the CancellationTokenSource.
            cts = new CancellationTokenSource();
        }

        public async Task<T> GetWithRetryAsync<T>(Uri baseUri, UriTemplate template, Dictionary<string,string> parameters)
        {
            Uri uri = template.BindByName(baseUri, parameters);
            string jsonContent = string.Empty;
            T content = default(T);

            if (xRateLimitRemaining < 10) await Task.Delay(2000);

            var response = await InvokeWebOperationWithRetry<HttpResponseMessage>( async () =>
                {
                    IEnumerable<string> headers = null;
                    var httpResponse = await httpClient.GetAsync(uri);

                    if (httpResponse.Headers.TryGetValues("X-RateLimit-Remaining", out headers))
                    {
                        xRateLimitRemaining = int.Parse(headers.First());
                        Debug.WriteLine(string.Format("X-RateLimit-Remaining: {0}", xRateLimitRemaining.ToString()));
                    }

                    if (httpResponse.Headers.TryGetValues("X-Total-Count", out headers))
                    {
                        var xTotalCount = int.Parse(headers.First());
                        Debug.WriteLine(string.Format("X-Total-Count: {0}", xTotalCount.ToString()));
                    }

                    httpResponse.EnsureSuccessStatusCode();
                    return httpResponse;                    
                }
            );

            jsonContent = await response.Content.ReadAsStringAsync();

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;

            try
            {
                content = JsonConvert.DeserializeObject<T>(jsonContent, settings);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return content;
        }

        private async static Task<T> InvokeWebOperationWithRetry<T>(Func<Task<T>> retriableOperation)
        {
            int baselineDelay = 1000;
            const int maxAttempts = 4;

            Random random = new Random();

            int attempt = 0;

            while (++attempt <= maxAttempts)
            {
                try
                {
                    return await retriableOperation();
                }
                catch (Exception ex)
                {
                    if (attempt == maxAttempts || !IsTransientException(ex))
                    {
                        throw;
                    }

                    int delay = baselineDelay + random.Next((int)(baselineDelay * 0.5), baselineDelay);

                    await Task.Delay(delay);

                    // Increment base-delay time
                    baselineDelay *= 2;
                }
            }

            // The logic above assures that this exception will never be thrown.
            throw new InvalidOperationException("This exception statement should never be thrown.");
        }

        private static bool IsTransientException(Exception ex)
        { 
            return true;
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            //if (filter != null)
            //{
            //    filter.Dispose();
            //    filter = null;
            //}

            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }

            if (cts != null)
            {
                cts.Dispose();
                cts = null;
            }
        }
    }
}
