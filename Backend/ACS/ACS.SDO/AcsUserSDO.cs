using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class CreateAndGrantUserSDO
    {
        public CreateAndGrantUserSDO() { }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AppCode { get; set; }
        public string RoleCode { get; set; }
    }
}
