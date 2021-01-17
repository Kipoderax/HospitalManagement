﻿using Dna;

namespace HospitalManagement.Core
{
    /// <summary>
    /// Extension methods for the <see cref="WebRequestResultExtensions"/> class
    /// </summary>
    public static class WebRequestResultExtensions
    {
        /// <summary>
        /// Checks the web request result for any errors, displaying them if there are any
        /// </summary>
        /// <typeparam name="T">The type of Api Response</typeparam>
        /// <param name="response">The response to check</param>
        /// <returns>Returns true if there was an error, or false if all was ok</returns>
        public static bool DisplayErrorIfFailedAsync ( this WebRequestResult response, string errorMessage )
        {
            // If there was no response, bad data or a response with a error message
            if (response == null || response.ServerResponse == null || (response.ServerResponse as ApiResponse)?.Successful == false)
            {
                // Default error message
                // TODO: Localize strings
                var message = "Unknown error from server call";

                // If we got a response from the server
                if (response?.ServerResponse is ApiResponse apiResponse)
                    // Set message to servers response
                    message = apiResponse.ErrorMessage;

                // If we have a result but deserialize failed
                else if (string.IsNullOrWhiteSpace( response?.RawServerResponse ))
                    // Set error message
                    message = $"Unexpected response from server. {response.RawServerResponse}";

                // If we have a result but no server response details at all...
                else if (response != null)
                    // Set message to standard HTTP server response details
                    message = response.ErrorMessage ?? $"{response.StatusCode}. ({response.StatusDescription})";

                // Display error
                //TODO: Localize string
                response.ErrorMessage = $" {errorMessage} - {message}.";

                // Enough failures
                return true;
            }

            return false;
        }
    }
}
