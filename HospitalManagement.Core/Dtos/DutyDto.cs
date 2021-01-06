using System;

namespace HospitalManagement.Core
{
    public class DutyDto
    {
        public Employee Employee { get; set; }
        public DateTime StartShift { get; set; }
        public DateTime EndShift { get; set; }
    }
}