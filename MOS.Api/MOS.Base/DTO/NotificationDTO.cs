using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOS.Base.DTO
{
    public class NotificationDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string EmployeRole { get; set; }
        public int EmployeNumber { get; set; }

    }
}