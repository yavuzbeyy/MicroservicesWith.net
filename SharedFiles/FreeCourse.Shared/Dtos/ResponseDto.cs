﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool isSuccesfull { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Data = data,
                isSuccesfull = true
            };
        }

        public static ResponseDto<T> Success(int statusCode) 
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                isSuccesfull = true
            };

        }

        public static ResponseDto<T> Fail(List<string> errors,int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                isSuccesfull = false
            };
        }

        public static ResponseDto<T> Fail(int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = new List<string>(),
                StatusCode = statusCode,
                isSuccesfull = false
            };
        }

    }
}
