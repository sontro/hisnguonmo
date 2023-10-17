using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using HIS.UC.SecondaryIcd;
using Inventec.Core;
using Inventec.Common.Controls.EditorLoader;
using DevExpress.XtraEditors;
using MOS.Filter;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.ApprovalSurgery.Base;
using HIS.Desktop.IsAdmin;
using HIS.Desktop.Utility;

namespace HIS.Desktop.Plugins.ApprovalSurgery
{
    public partial class UCApprovalSurgery : UserControlBase
    {
        private bool CheckApprovalPlan(V_HIS_SERE_SERV_13 sereServ13, APPROVAL_PLAN_ACTION action)
        {
            bool result = false;
            try
            {
                if (sereServ13 != null)
                {
                    if (action == APPROVAL_PLAN_ACTION.APPROVAL && CheckControlRole(ControlRoleCode.DUYET_KE_HOACH))
                    {
                        if (sereServ13.PTTT_CALENDAR_ID.HasValue
                            && sereServ13.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW)
                            result = true;
                    }
                    else if (action == APPROVAL_PLAN_ACTION.UNAPPROVAL && CheckControlRole(ControlRoleCode.HUY_DUYET_KE_HOACH))
                    {
                        if (sereServ13.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED)
                            result = true;
                    }
                    else if (action == APPROVAL_PLAN_ACTION.REJECT
                        && CheckControlRole(ControlRoleCode.TU_CHOI_DUYET_KE_HOACH))
                    {
                        if (sereServ13.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW)
                            result = true;
                    }
                    else if (action == APPROVAL_PLAN_ACTION.UNREJECT
                        && CheckControlRole(ControlRoleCode.HUY_TU_CHOI_DUYET_KE_HOACH))
                    {
                        if (sereServ13.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__REJECTED)
                            result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private bool CheckApproval(V_HIS_PTTT_CALENDAR ptttCalendarTemp, ref ACTION_APPROVAL_CALENDAR? action)
        {
            bool result = false;
            try
            {
                if (ptttCalendarTemp != null)
                {
                    string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();

                    if (ptttCalendarTemp.APPROVAL_TIME.HasValue)
                    {
                        action = ACTION_APPROVAL_CALENDAR.UNAPPROVAL;
                        if ((ptttCalendarTemp.APPROVAL_LOGINNAME == loginName || CheckLoginAdmin.IsAdmin(loginName))
                            && CheckControlRole(ControlRoleCode.HUY_DUYET_LICH))
                            result = true;
                    }
                    else
                    {
                        action = ACTION_APPROVAL_CALENDAR.APPROVAL;
                        if (CheckControlRole(ControlRoleCode.DUYET_LICH))
                            result = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private bool CheckApprovalEdit(V_HIS_PTTT_CALENDAR ptttCalendarTemp)
        {
            bool result = false;
            try
            {
                string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                if (!ptttCalendarTemp.APPROVAL_TIME.HasValue 
                    && (ptttCalendarTemp.CREATOR == loginName || CheckLoginAdmin.IsAdmin(loginName)))
                    result = true;

            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private bool CheckDelete(V_HIS_PTTT_CALENDAR ptttCalendarTemp)
        {
            bool result = false;
            try
            {
                string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                if (!ptttCalendarTemp.APPROVAL_TIME.HasValue
                    && (ptttCalendarTemp.CREATOR == loginName || CheckLoginAdmin.IsAdmin(loginName)))
                    result = true;

            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private bool CheckControlRole(string controlRoleCode)
        {
            bool result = false;
            try
            {
                List<string> controlRuleCodes = controlAcss != null ? controlAcss.Select(o => o.CONTROL_CODE).ToList() : null;
                if (controlRuleCodes != null && controlRuleCodes.Contains(controlRoleCode))
                    result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
