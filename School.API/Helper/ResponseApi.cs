﻿namespace School.API.Helper
{
    public class ResponseApi
    {
        public ResponseApi(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetFormStatusCode(StatusCode);

        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        private string GetFormStatusCode(int statuscode)
        {
            return statuscode switch
            {
                200 => "Done",
                400 => "Bad Request",
                404 => "Not Found Resours",
                401 => "UnAuthorized",
                500 => "server Error",
                _ => null,
            };
        }
    }
}
