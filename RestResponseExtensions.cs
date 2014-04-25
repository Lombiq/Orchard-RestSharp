using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RestSharp
{
    public static class RestResponseExtensions
    {
        public static void ThrowIfTransportFailed(this IRestResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.TimedOut)
                throw new WebException("The request to " + response.Request.Resource + " timed out.", response.ErrorException);

            if (response.ResponseStatus == ResponseStatus.Error)
                throw new WebException("The request to " + response.Request.Resource + " failed with the following status: " + response.StatusDescription, response.ErrorException);

            if (response.ResponseStatus == ResponseStatus.Aborted)
                throw new WebException("The request to " + response.Request.Resource + " was aborted.", response.ErrorException);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new WebException("The request to " + response.Request.Resource + " is unauthorized.", response.ErrorException);
        }

        public static void ThrowIfStatusCodeNotOk(this IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new WebException("The request to " + response.Request.Resource + " returned the status code " + response.StatusCode + ".");
        }
    }
}