﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class StringFormatAttribute : ValidationAttribute
    {
        public string Format { get; set; }
        public StringFormatAttribute(string friendlyName, string format, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            Format = format;
        }
    }
}
