using HIS.Desktop.IsAdmin;
using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Linq;

namespace HIS.Desktop.Plugins.ServiceReqUpdateInstruction
{
    public partial class frmServiceReqUpdateInstruction : HIS.Desktop.Utility.FormBase
    {
        private void InitEnabledControl()
        {
            try
            {
                long currentDepartmentId = HIS.Desktop.LocalStorage.LocalData.WorkPlace.WorkPlaceSDO.FirstOrDefault(o => o.RoomId == module.RoomId).DepartmentId;
                if (currentServiceReq == null)
                {
                    Inventec.Common.Logging.LogSystem.Error("Khong tim thay currentServiceReq");
                    return;
                }

                if (currentServiceReq.REQUEST_DEPARTMENT_ID == currentDepartmentId)
                {
                    lciThoiGianYLenh.Enabled = true;
                    panelControlUcIcd.Enabled = true;
                    panelControlSubIcd.Enabled = true;
                    panelControlCauseIcd.Enabled = true;
                    btnSave.Enabled = true;
                }

                if (currentServiceReq.EXECUTE_DEPARTMENT_ID == currentDepartmentId)
                {
                    lciStartTime.Enabled = true;
                    lciEndTime.Enabled = true;
                    lciUserName.Enabled = true;
                    lciCboNguoiThucHien.Enabled = true;
                    btnSave.Enabled = true;
                }

                if (HisConfigs.Get<string>("HIS.Desktop.Plugins.AssignConfig.ShowRequestUser") == "1")
                {
                    cboRequestUser.Enabled = true;
                    txtRequestUser.Enabled = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    cboRequestUser.Enabled = false;
                    txtRequestUser.Enabled = false;
                }

                string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();

                if (CheckLoginAdmin.IsAdmin(loginName))
                {
                    lciThoiGianYLenh.Enabled = true;
                    lciStartTime.Enabled = true;
                    lciEndTime.Enabled = true;
                    lciUserName.Enabled = true;
                    lciCboNguoiThucHien.Enabled = true;
                    panelControlSubIcd.Enabled = true;
                    panelControlUcIcd.Enabled = true;
                    panelControlCauseIcd.Enabled = true;
                    btnSave.Enabled = true;
                }

                if (currentServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    lciStartTime.Enabled = false;
                    lciEndTime.Enabled = false;
                }
                else if (currentServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL)
                {
                    lciStartTime.Enabled = true;
                    lciEndTime.Enabled = false;
                    btnSave.Enabled = true;
                }
                if (currentServiceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK
                    || currentServiceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM
                    || currentServiceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT
                    ||currentServiceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT)
                {
                    cboEndServiceReq.Enabled = false;
                    txtLoginname.Enabled = false;
                }
                if (currentServiceReq.CREATOR == LoggingName || CheckLoginAdmin.IsAdmin(loginName))
                {
                    mmNOTE.Enabled = true;
                }
                else
                {
                    mmNOTE.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
