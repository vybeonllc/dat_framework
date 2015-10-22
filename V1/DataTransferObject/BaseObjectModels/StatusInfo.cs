using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()]
    public class StatusInfo
    {
        public StatusInfo()
        {
        }
        public StatusInfo(System.Net.HttpStatusCode code)
        {
            Code = (int)code;
            Message = code.ToString().Replace("_", "");
        }

        [DataMember(Name = "message")] 
        public string Message { get; set; }

        /// <summary>
        /// _blank, _parent,_self,...
        /// </summary>
        [DataMember(Name = "target")]
        public string Target { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        public override string ToString()
        {
            return Code + ": " + Message;
        }
    }
}