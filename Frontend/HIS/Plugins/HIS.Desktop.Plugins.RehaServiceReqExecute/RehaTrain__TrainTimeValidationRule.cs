using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.RehaServiceReqExecute
{
    class RehaTrain__TrainTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtTrainTime;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtTrainTime == null) return valid;
                if (String.IsNullOrEmpty(dtTrainTime.Text))
                    return valid;
                string date = string.Format("{0:00}", Inventec.Common.TypeConvert.Parse.ToInt32(dtTrainTime.Text)) + "" + string.Format("{0:00}", Inventec.Common.TypeConvert.Parse.ToInt32(dtTrainTime.Text)) + "" + string.Format("{0:00}", Inventec.Common.TypeConvert.Parse.ToInt32(dtTrainTime.Text)) + "000000";
                //string date = Inventec.Common.DateTime.Convert.SystemDateTimeToDateString(dtRequestDate_ExamPage);
                valid = Inventec.Common.DateTime.Check.IsValidTime(date);
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
