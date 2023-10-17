using HTC.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseTypeList.ADO
{
    public class ExpenseTypeADO : HTC_EXPENSE_TYPE
    {
        public bool AllowCreateExpense { get; set; }
        public bool IsPlus { get; set; }
        public string CreateTimeStr { get; set; }
        public string ModifyTimeStr { get; set; }

        public ExpenseTypeADO() { }

        public ExpenseTypeADO(HTC_EXPENSE_TYPE data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HTC_EXPENSE_TYPE>(this, data);
                    this.AllowCreateExpense = (this.IS_ALLOW_EXPENSE == IMSys.DbConfig.HTC_RS.HTC_EXPENSE_TYPE.IS_ALLOW_EXPENSE__TRUE);
                    this.IsPlus = (this.IS_PLUS == IMSys.DbConfig.HTC_RS.HTC_EXPENSE_TYPE.IS_PLUS__TRUE);
                    this.CreateTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.CREATE_TIME ?? 0);
                    this.ModifyTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.MODIFY_TIME ?? 0);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
