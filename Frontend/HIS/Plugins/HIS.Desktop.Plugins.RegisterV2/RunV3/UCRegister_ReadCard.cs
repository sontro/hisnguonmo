using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Utility;
using System.Threading;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Common.Adapter;
using MOS.SDO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
    public partial class UCRegister : UserControlBase
    {
        //public UCRegister_ReadCard()
        //{
        //    InitializeComponent();
        //}
        private void CreateThreadInitWCFReadCard()
        {
            Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(InitWCFReadCardThread));
            try
            {
                thread.Start();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                thread.Abort();
            }
        }

        private void InitWCFReadCardThread()
        {
            try
            {
                CARD.WCF.Service.TapCardService.TapCardServiceManager.OpenHost();
                CARD.WCF.Service.TapCardService.TapCardServiceManager.SetDelegate(this.CheckServiceCodeDelegate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        bool CheckServiceCodeDelegate(string serviceCode)
        {
            bool success = false;
            try
            {
                this.SearchAndFillDataCardInfo(serviceCode);
                success = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return success;
        }

        void SearchAndFillDataCardInfo(string serviceCode)
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("serviceCode: " + serviceCode);
                if (ucAddressCombo1 != null)
                    ucAddressCombo1.GetPatientSdo(null);
                CommonParam param = new CommonParam();
                var patientInRegisterSearchByCard = new BackendAdapter(param).Get<HisCardSDO>(RequestUriStore.HIS_CARD_GETVIEWBYSERVICECODE, ApiConsumers.MosConsumer, serviceCode, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, param);
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => patientInRegisterSearchByCard), patientInRegisterSearchByCard));
                if (patientInRegisterSearchByCard != null)
                { 
                    var data = this.SearchByCode(patientInRegisterSearchByCard.PatientCode);
                    if (data != null && data.Result != null && data.Result is HisPatientSDO)
                    {
                        //xuandv --- ThongBaoCu
                        // DevExpress.Utils.WaitDialogForm waitLoad = new DevExpress.Utils.WaitDialogForm(MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao), MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongThongBaoMoTaChoWaitDialogForm));
                        //Benh nhan da dang ky tren he thong benh vien, da co thong tin ho so
                        //this.SetPatientSearchPanel(true);
                        HisPatientSDO patientSDO = (HisPatientSDO)(data.Result);
                        patientSDO.HT_ADDRESS = patientInRegisterSearchByCard.HtAddress;
                        patientSDO.HT_COMMUNE_NAME = patientInRegisterSearchByCard.HtCommuneName;
                        patientSDO.HT_DISTRICT_NAME = patientInRegisterSearchByCard.HtDistrictName;
                        patientSDO.HT_PROVINCE_NAME = patientInRegisterSearchByCard.HtProvinceName;
                        if (ucAddressCombo1 != null)
                            ucAddressCombo1.GetPatientSdo(patientSDO);
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            this.ProcessPatientCodeKeydown(patientSDO);
                            this.FillDataIntoUCPlusInfo(patientSDO);
                        }));
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(delegate()
                        {

                            //An button lam moi khi co du lieu benh nhan cu
                            this.SetPatientSearchPanel(false);

                            //Benh nhan chua dang ky tren he thong benh vien, chua co thong tin ho so
                            HisPatientSDO patientByCard = new HisPatientSDO();
                            this.SetPatientDTOFromCardSDO(patientInRegisterSearchByCard, patientByCard);
                            this.FillDataPatientToControl(patientByCard);

                            this.FillDataToHeinCardControlByCardSDO(patientInRegisterSearchByCard);
                            //try
                            //{
                            //    if (!waitLoad.IsDisposed) waitLoad.Invoke(new MethodInvoker(delegate() { waitLoad.Dispose(); }));
                            //}
                            //catch (Exception ex)
                            //{
                            //    LogSystem.Debug("Dispose waitLoad fail.", ex);
                            //}
                        }));
                    }
                    this.cardSearch = patientInRegisterSearchByCard;
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        this.cardSearch = null;
                        if (param.Messages == null || param.Messages.Count == 0)
                        {
                            param.Messages.Add(ResourceMessage.ThongBaoKetQuaTimKiemBenhNhanKhiQuetTheDuLieuTraVeNull);
                        }
                        MessageManager.Show(param, null);
                    }));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
