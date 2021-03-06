﻿using System;
using System.Net;
using UrbanEngine.SharedKernel.Attributes;

namespace UrbanEngine.SharedKernel.Results
{
    public class FailureResult : ResultBase
    {
        public override string ResultType => "Failure";

        public FailureResult(Exception ex)
        {
            Message = GetMessageToShow(ex);
            StatusCode = GetStatusCode(ex);
            Success = false;
        }

        public FailureResult(string message, int statusCode = 500)
        {
            Success = false;
            StatusCode = statusCode;
            Message = message;
        }

        public static string GetMessageToShow(Exception ex)
        {
            if (ex == null)
                return "An error occurred";

			if(IsCustomException(ex))
				return ex.Message;
            else if (ex is ArgumentException || ex is ArgumentNullException)
                return "Invalid arguments were specified, please check your request and try again";
            else if (ex is NotImplementedException)
                return "The requested operation is not supported at this time";
            else
                return "An error occurred trying to process your request, please try again";
        }
        
        public static int GetStatusCode(Exception ex)
        {
            HttpStatusCode httpStatusCode;
            
            if (ex is ArgumentException || ex is ArgumentNullException)
                httpStatusCode = HttpStatusCode.BadRequest;
            else
                httpStatusCode = HttpStatusCode.InternalServerError;

            return (int)httpStatusCode;
        }

		public static bool IsCustomException(Exception ex)
		{
			var isCustomAttribute = Attribute.GetCustomAttribute(ex.GetType(), typeof(CustomExceptionAttribute));
			return isCustomAttribute != null;
		}
    }
}
