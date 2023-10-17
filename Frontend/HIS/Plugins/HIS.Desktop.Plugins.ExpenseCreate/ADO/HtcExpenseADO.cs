using HTC.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseCreate.ADO
{
    public class HtcExpenseADO : HTC_EXPENSE
    {
        public bool IsError { get; set; }
        public string DESCRIPTION { get; set; }

        public HtcExpenseADO() { }

        public HtcExpenseADO(HIS_DEPARTMENT department)
        {
            try
            {
                if (department != null)
                {
                    this.DEPARTMENT_CODE = department.DEPARTMENT_CODE;
                    this.DEPARTMENT_NAME = department.DEPARTMENT_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
