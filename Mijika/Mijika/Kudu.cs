﻿using System.Collections.Generic;
using Mijika.Tokens;

namespace Mijika
{
    public class Kudu
    {        
        public string Project { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BaseUrl => $"https://{Project}.scm.azurewebsites.net:";

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="project">Project.</param>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public Kudu(string project, string username, string password)
        {
            Project = project;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Get list of web jobs.
        /// </summary>
        public List<WebJob> GetWebJobs()
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            return rest.Get<List<WebJob>>("/api/webjobs");
        }

        /// <summary>
        /// Get list of triggered web jobs.
        /// </summary>
        public List<WebJob> GetTriggeredWebJobs()
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            return rest.Get<List<WebJob>>("/api/triggeredwebjobs");
        }

        /// <summary>
        /// Get list of continuous web jobs.
        /// </summary>
        public List<WebJob> GetContinuousWebJobs()
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            return rest.Get<List<WebJob>>("/api/continuouswebjobs");
        }

        /// <summary>
        /// Get triggered web job.
        /// </summary>
        /// <param name="name">Job name.</param>
        public WebJob GetTriggeredWebJob(string name)
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            return rest.Get<WebJob>($"/api/triggeredwebjobs/{name}");
        }

        /// <summary>
        /// Get continuous web job.
        /// </summary>
        /// <param name="name">Job name.</param>
        public WebJob GetContinuousWebJob(string name)
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            return rest.Get<WebJob>($"/api/continuouswebjobs/{name}");
        }

        /// <summary>
        /// Get triggered web job history.
        /// </summary>
        /// <param name="name">Job name.</param>
        public History GetTriggeredWebJobHistory(string name)
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            return rest.Get<History>($"/api/triggeredwebjobs/{name}/history");
        }

        /// <summary>
        /// Invoke triggered job.
        /// </summary>
        /// <param name="name">Job name.</param>
        /// <returns>Location.</returns>
        public System.Uri InvokeTriggeredJob(string name)
        {
            return InvokeTriggeredJob(name, null);
        }

        /// <summary>
        /// Invoke triggered job.
        /// </summary>
        /// <param name="name">Job name.</param>
        /// <param name="arguments">Arguments.</param>
        /// <returns>Location.</returns>
        /// <remarks>Arguments are accessible as the WEBJOBS_COMMAND_ARGUMENTS environment variable.</remarks>
        public System.Uri InvokeTriggeredJob(string name, string arguments)
        {
            var rest = new RestApi(BaseUrl, Username, Password);
            var arg = string.Empty;

            if (!string.IsNullOrWhiteSpace(arguments))
                arg = "?arguments=" + System.Net.WebUtility.UrlEncode(arguments);

            var headers = rest.Post<System.Net.Http.Headers.HttpResponseHeaders>($"/api/triggeredwebjobs/{name}/run{arg}");

            return headers?.Location;
        }

        /// <summary>
        /// Get log.
        /// </summary>
        /// <param name="outputLogUrl">Full url to log.</param>
        public string GetLog(string outputLogUrl)
        {
            var rest = new RestApi(outputLogUrl, Username, Password);
            return rest.Get();
        }        
    }
}
