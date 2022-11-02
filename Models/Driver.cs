using System;
using System.Collections.Generic;

namespace net_redisAPI.Models
{
    public partial class Driver
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? DriveNb { get; set; }
    }
}
