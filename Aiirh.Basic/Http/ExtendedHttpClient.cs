using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Http.Extensions;
using Aiirh.Basic.Http.RequestLogging;
using Aiirh.Basic.Logging;
using Aiirh.Basic.Utilities;
using Newtonsoft.Json;

namespace Aiirh.Basic.Http
{
    public class ExtendedHttpClient : HttpClient
    {
        private readonly IRequestLogSystemWriter _requestLogWriter;
        private readonly ILogger _logger;

        public bool IsDisposed { get; private set; }

        static ExtendedHttpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public ExtendedHttpClient(HttpMessageHandler handler, IRequestLogSystemWriter requestLogWriter, ILogger logger) : base(handler)
        {
            _requestLogWriter = requestLogWriter;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendAsXmlAsync<TRequest>(string url, TRequest data, HttpRequestDescription description, bool skipMessageLog = false)
        {
            var (httpContent, requestText) = GetXmlContent(data);
            var stopWatch = Stopwatch.StartNew();
            HttpResponseMessage response = null;
            try
            {
                var request = new HttpRequestMessage(description.Method.ToHttpMethod(), url)
                {
                    Content = httpContent
                };

                if (description.AuthenticationHeader != null)
                {
                    request.Headers.Authorization = description.AuthenticationHeader;
                }

                if (description.DefaultHttpHeaders?.Count > 0)
                {
                    foreach (var header in description.DefaultHttpHeaders)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                response = await SendAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(description.Action, ex.LogException());
                throw;
            }
            finally
            {
                var elapsedTime = stopWatch.ElapsedMilliseconds;
                if (_requestLogWriter != null && description != null)
                {
                    var messageLogData = new RequestLogSystemData(requestText)
                    {
                        Response = response,
                        ElapsedTime = elapsedTime,
                        SkipIfSuccessful = skipMessageLog
                    };
                    await _requestLogWriter.WriteMessageLog(description, messageLogData);
                }
            }
        }

        public async Task<HttpResponseMessage> SendAsJsonAsync<TRequest>(string url, TRequest data, HttpRequestDescription description, bool skipMessageLog = false)
        {
            var (httpContent, requestText) = GetJsonContent(data);
            var stopWatch = Stopwatch.StartNew();
            HttpResponseMessage response = null;
            try
            {
                var request = new HttpRequestMessage(description.Method.ToHttpMethod(), url)
                {
                    Content = httpContent
                };

                if (description.AuthenticationHeader != null)
                {
                    request.Headers.Authorization = description.AuthenticationHeader;
                }

                if (description.DefaultHttpHeaders?.Count > 0)
                {
                    foreach (var header in description.DefaultHttpHeaders)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                response = await SendAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(description.Action, ex.LogException());
                throw;
            }
            finally
            {
                var elapsedTime = stopWatch.ElapsedMilliseconds;
                if (_requestLogWriter != null && description != null)
                {
                    var messageLogData = new RequestLogSystemData(requestText)
                    {
                        Response = response,
                        ElapsedTime = elapsedTime,
                        SkipIfSuccessful = skipMessageLog
                    };
                    await _requestLogWriter.WriteMessageLog(description, messageLogData);
                }
            }
        }

        public async Task<HttpResponseMessage> SendAsFormAsync(string url, ICollection<KeyValuePair<string, string>> formData, HttpRequestDescription description, bool skipMessageLog = false)
        {
            var stopWatch = Stopwatch.StartNew();
            HttpResponseMessage response = null;
            try
            {
                var request = new HttpRequestMessage(description.Method.ToHttpMethod(), url)
                {
                    Content = formData != null ? new FormUrlEncodedContent(formData) : null
                };

                if (description.AuthenticationHeader != null)
                {
                    request.Headers.Authorization = description.AuthenticationHeader;
                }

                if (description.DefaultHttpHeaders?.Count > 0)
                {
                    foreach (var header in description.DefaultHttpHeaders)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                response = await SendAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(description.Action, ex.LogException());
                throw;
            }
            finally
            {
                var elapsedTime = stopWatch.ElapsedMilliseconds;
                if (_requestLogWriter != null && description != null)
                {
                    var form = string.Join(";", formData?.Select(x => $"{x.Key}:{x.Value}") ?? Enumerable.Empty<string>());
                    var messageLogData = new RequestLogSystemData(form)
                    {
                        Response = response,
                        ElapsedTime = elapsedTime,
                        SkipIfSuccessful = skipMessageLog
                    };
                    await _requestLogWriter.WriteMessageLog(description, messageLogData);
                }
            }
        }

        private static (HttpContent content, string requestText) GetJsonContent<TRequest>(TRequest data)
        {
            if (data == null)
            {
                return (null, null);
            }
            var requestText = JsonConvert.SerializeObject(data);
            var content = new StringContent(requestText);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return (content, requestText);
        }

        private static (HttpContent content, string requestText) GetXmlContent<TRequest>(TRequest data)
        {
            if (data == null)
            {
                return (null, null);
            }
            var requestText = XmlUtility.Serialize(data, new XmlWriterSettings { Indent = false, Encoding = Encoding.UTF8 });
            var content = new StringContent(requestText);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            return (content, requestText);
        }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }
}
