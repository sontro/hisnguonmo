using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Data;
using HIS.Desktop.Utility;
using HIS.UC.WorkPlace;
using IMSys.DbConfig.HIS_RS;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.SickLeave
{
    public partial class frmSickLeave : FormBase
    {
        public void SetValue(object data)
        {
            try
            {
                if (data != null && data is TreatmentEndTypeExtData)
                {
                    TreatmentEndTypeExtData sickLeaveData = data as TreatmentEndTypeExtData;
                    if (sickLeaveData.SickLeaveDay.HasValue)
                        spinSickLeaveDay.Value = sickLeaveData.SickLeaveDay.Value;
                    if (sickLeaveData.SickLeaveFrom.HasValue)
                        dtSickLeaveFromTime.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sickLeaveData.SickLeaveFrom.Value) ?? DateTime.Now;
                    if (sickLeaveData.SickLeaveTo.HasValue)
                        dtSickLeaveToTime.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sickLeaveData.SickLeaveTo.Value) ?? DateTime.Now;
                    txtRelativeName.Text = sickLeaveData.PatientRelativeName;
                    cboRelativeType.EditValue = sickLeaveData.PatientRelativeType;
                    txtPatientWorkPlace.Text = sickLeaveData.PatientWorkPlace;
                    cboDocumentBook.EditValue = sickLeaveData.DocumentBookId;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
