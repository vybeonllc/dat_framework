﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Subscription
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }

        public Manifest()
        {
        }

    }
}
