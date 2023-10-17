using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo
{
    internal class SessionIdGenerate
    {
        private static string sessionId { get; set; }
        public static string SessionId
        {
            get
            {
                if (String.IsNullOrWhiteSpace(sessionId))
                {
                    sessionId = Guid.NewGuid().ToString();
                }

                return sessionId;
            }
        }
    }
}
