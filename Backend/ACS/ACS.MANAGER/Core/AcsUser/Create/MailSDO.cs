using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.Core.AcsUser.Create
{
    internal class MailSDO
    {
        internal MailSDO() { }
        public string LoginName { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
    }
}
