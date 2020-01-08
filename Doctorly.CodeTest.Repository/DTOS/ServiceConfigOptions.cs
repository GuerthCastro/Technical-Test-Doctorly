using System;
using System.Collections.Generic;
using System.Text;

namespace Doctorly.CodeTest.Repository.DTOS
{
    public class ServiceConfigOptions
    {
        public string ServiceName { get; set; }
        public string ServiceVersion { get; set; }
        public string OS { get; set; }
        public string Environment { get; set; }
        public DateTime CurrentDate { get { return DateTime.Now; } }
    }
}
