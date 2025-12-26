using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
namespace CoreApplication.API.Helpers
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string infoMessage);
        void LogError(Uri BaseUrl, IRestRequest request, IRestResponse response);
    }
    public class ErrorLogger : IErrorLogger
    {

            public void LogError(Exception ex, string infoMessage)
            {
                // Log the error to your error database ...
                // Do whatever the error logging can do ...
            }

            public void LogError(Uri BaseUrl, IRestRequest request, IRestResponse response)
            {
                //Get the values of the parameters passed to the API
                string parameters = string.Join(", ", request.Parameters.Select(x => x.Name.ToString() + "=" + ((x.Value == null) ? "NULL" : x.Value)).ToArray());

                //Set up the information message with the URL, the status code, and the parameters.
                string info = "Request to " + BaseUrl.AbsoluteUri + request.Resource + " failed with status code " + response.StatusCode + ", parameters: "
                              + parameters + ", and content: " + response.Content;

                //Acquire the actual exception
                Exception ex;
                if (response != null && response.ErrorException != null)
                {
                    ex = response.ErrorException;
                }
                else
                {
                    ex = new Exception(info);
                    info = string.Empty;
                }

                //Log the exception and info message
                this.LogError(ex, info);
            }
        }
    }

