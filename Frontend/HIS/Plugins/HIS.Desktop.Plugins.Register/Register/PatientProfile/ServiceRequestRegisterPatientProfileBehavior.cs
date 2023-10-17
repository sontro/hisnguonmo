using AutoMapper;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.Register.Run;
using HIS.Desktop.Utility;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.Register
{
    class ServiceRequestRegisterPatientProfileBehavior : ServiceRequestRegisterBehaviorBase, IServiceRequestRegisterPatientProfile
    {
        HisPatientProfileSDO result = null;
        internal ServiceRequestRegisterPatientProfileBehavior(CommonParam param, UCRegister ucServiceRequestRegiter, HisPatientSDO patientData)
            : base(param, ucServiceRequestRegiter)
        {
        }

        HisPatientProfileSDO IServiceRequestRegisterPatientProfile.Run()
        {
            this.patientProfile = new HisPatientProfileSDO();
            this.patientProfile.HisPatient = new MOS.EFMODEL.DataModels.HIS_PATIENT();
            this.patientProfile.HisPatientTypeAlter = new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE_ALTER();
            this.patientProfile.HisTreatment = new MOS.EFMODEL.DataModels.HIS_TREATMENT();
            //Process common data
            base.InitBase();
            if (!string.IsNullOrEmpty(patientProfile.HisPatientTypeAlter.LIVE_AREA_CODE))
            {
                Inventec.Desktop.Common.Message.WaitingManager.Hide();
                if (DevExpress.XtraEditors.XtraMessageBox.Show(
                ResourceMessage.BanCoMuonNhapThongTinKhuVuc,
                ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    //Neu chon khong
                    // focus con tro vao o khu vuc
                    this.uCMainHein.SetFocusUserByLiveAreaCode(this.ucHein__BHYT);
                    this.ucRequestService.isShowMess = true;
                    return null;
                }
                Inventec.Desktop.Common.Message.WaitingManager.Show();
            }
            if (HisConfigCFG.IsCheckExamination)
            {
                if (this.ucRequestService.serviceReqDetailSDOs != null && this.ucRequestService.serviceReqDetailSDOs.Count > 0)
                {
                    //TODO
                }
                else
                {
                    Inventec.Desktop.Common.Message.WaitingManager.Hide();
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(
                                ResourceMessage.BenhNhanChuaChonCongKham,
                                ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.ucRequestService.isShowMess = true;
                        return null;
                    }
                    Inventec.Desktop.Common.Message.WaitingManager.Show();
                }
            }
            //Execute call api

            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => patientProfile), patientProfile));
            result = (HisPatientProfileSDO)base.RunBase(this.patientProfile, this.ucRequestService);
            if (result == null)
            {
                Inventec.Common.Logging.LogSystem.Warn("Goi api dang ky tiep don that bai, Dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => patientProfile), patientProfile) + ", Dau ra____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + "__" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
            }
            return result;
        }
    }
}
