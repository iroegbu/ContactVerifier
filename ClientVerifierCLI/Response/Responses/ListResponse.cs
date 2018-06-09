﻿using ClientVerifierLibrary.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientVerifierCLI.Response.Responses
{
    class ListResponse : IResponse
    {
        public string ResponseMessage { get; set; }
        public List<ContactEntity> Payload { get; set; }
        public TimeSpan ResponseTime { get; set; }

        object IResponse.Payload()
        {
            return Payload;
        }

        string IResponse.ResponseMessage()
        {
            return ResponseMessage;
        }

        TimeSpan IResponse.ResponseTime()
        {
            return ResponseTime;
        }
    }
}
