using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace collage_interview_tasks
{
    public abstract class HttpRequestHandler<TResponse>
    {
        private HttpClient httpClientProxy;
        private IHttpResponseDeserializer<TResponse> parser { get; }

        protected HttpRequestHandler(HttpClient _httpClientProxy, IHttpResponseDeserializer<TResponse> _parser)
        {
            parser = _parser;
            httpClientProxy = _httpClientProxy;
        }

        public TResponse Handle(HttpMethod methodName, string url, object payload, IDictionary<string, string> additionalHeaders)
        {
            var httpRequestMessage = new HttpRequestMessage(methodName, new Uri(url));

            if (payload != null)
                httpRequestMessage.Content = payload as HttpContent;

            if (additionalHeaders != null)
                foreach (var header in additionalHeaders)
                    httpRequestMessage.Headers.Add(header.Key, header.Value);

            HttpResponseMessage response = httpClientProxy.SendAsync(httpRequestMessage).Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                //throw new EndOfStreamException(string.Join(",", response.Headers));
            }
                
            try
            {
                return parser.Deserialize(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new NotImplementedException("todo: add missing implementation");
            }
        }

        public interface IHttpResponseDeserializer<out TResult>
        {
            TResult Deserialize(HttpResponseMessage response);
        }
    }
}
