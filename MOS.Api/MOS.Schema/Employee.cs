using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOS.Base.Schema;

namespace MOS.Schema
{
    public class AdminRequest : BaseEmployeeRequest
    {

    }
    public class AdminResponse: BaseEmployeeResponse
    {
        public int AdminNumber { get; set; }
    }
    public class PersonalRequest : BaseEmployeeRequest
    {
        public string IBAN { get; set; }
    }
    public class PersonalResponse: BaseEmployeeResponse
    {
        public int PersonalNumber { get; set; }
    }
}