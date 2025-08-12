﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profile_api.domain.Common
{
    public class Response<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Response()
        {
            
        }
        public Response(int code, string message, T? data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
