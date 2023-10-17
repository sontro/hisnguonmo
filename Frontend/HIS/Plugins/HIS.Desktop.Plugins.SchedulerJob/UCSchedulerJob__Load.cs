using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Common.Adapter;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using MOS.Filter;
using MOS.SDO;

namespace HIS.Desktop.Plugins.SchedulerJob
{
    public partial class UCSchedulerJob : HIS.Desktop.Utility.UserControlBase
    {
        private void LoadSchedulerJob()
        {
            try
            {
                UserSchedulerJobFilter filter = new UserSchedulerJobFilter();
                CommonParam param = new CommonParam();
                UserSchedulerJobResultSDOs = new BackendAdapter(param)
                    .Get<List<UserSchedulerJobResultSDO>>("api/UserSchedulerJob/Get", ApiConsumers.MosConsumer, filter, param);
                gridControlSchedulerJob.DataSource = UserSchedulerJobResultSDOs;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
