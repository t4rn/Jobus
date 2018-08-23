using Jobus.Common.Results;
using Jobus.Core.Services.Loggers;
using RestSharp;
using System;

namespace Jobus.Core.Services.Rest
{
    public class RestService : IRestService
    {
        private readonly ILogger _log;
        private readonly RestSettings _settings;

        public RestService(ILogger log, RestSettings settings)
        {
            _log = log;
            _settings = settings;
        }
        public Result SendRequest(int jobId, string body)
        {
            Result result = new Result();
            string methodName = nameof(SendRequest);

            var client = new RestClient(_settings.Url);
            var request = new RestRequest($"{jobId}", Method.PUT);

            Uri fullUrl = client.BuildUri(request);
            _log.LogDebug("{MethodName} Sending status for jobId = {JobId} to Url = {ApiUrl}", methodName, jobId, fullUrl);

            request.RequestFormat = DataFormat.Json;
            foreach (var header in _settings.Headers)
                request.AddHeader(header.Name, header.Value);

            request.AddBody(body);

            // ignore SSL errors
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            IRestResponse<OutputDto> response = client.Execute<OutputDto>(request);

            _log.LogDebug("{MethodName} Job with Id = {JobId} sent -> error = {ResponseError} | response = {JobResponse}",
                methodName, jobId, response.ErrorMessage, response.Content);

            result.IsOk = response.IsSuccessful;
            result.Message = !string.IsNullOrWhiteSpace(response.Data?.message) ? response.Data.message :
                !string.IsNullOrWhiteSpace(response.ErrorMessage) ? response.ErrorMessage : response.Content;

            return result;
        }
    }
}
