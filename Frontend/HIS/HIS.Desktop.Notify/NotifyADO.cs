using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Notify
{
    public class NotifyADO : SDA.EFMODEL.DataModels.SDA_NOTIFY
    {
        public bool Status { get; set; }
        public bool Action { get; set; }

        public NotifyADO(SDA_NOTIFY data)
        {
            this.ID = data.ID;
            this.CONTENT = data.CONTENT;
            this.CREATE_TIME = data.CREATE_TIME;
            this.CREATOR = data.CREATOR;
            this.MODIFY_TIME = data.MODIFY_TIME;
            this.MODIFIER = data.MODIFIER;
            this.FROM_TIME = data.FROM_TIME;
            this.TO_TIME = data.TO_TIME;
            this.TITLE = data.TITLE;
            this.RECEIVER_LOGINNAME = data.RECEIVER_LOGINNAME;
            this.LOGIN_NAMES = data.LOGIN_NAMES;
        }


        public void SetIsRead(string loginname)
        {
            if (!String.IsNullOrWhiteSpace(this.LOGIN_NAMES) && !String.IsNullOrWhiteSpace(loginname))
            {
                string readers = "," + this.LOGIN_NAMES + ",";
                this.Status = readers.Contains(("," + loginname + ","));
            }
        }

    }
}
