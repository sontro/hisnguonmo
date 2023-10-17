using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Print;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.LocalData;

namespace HIS.Desktop.Plugins.BidCreate
{
    public partial class UCBidCreate : HIS.Desktop.Utility.UserControlBase
    {

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnPrint.Enabled || bidModel == null)
                    return;
                Inventec.Common.RichEditor.RichEditorStore richEditor = new Inventec.Common.RichEditor.RichEditorStore(ApiConsumer.ApiConsumers.SarConsumer, HIS.Desktop.LocalStorage.ConfigSystem.ConfigSystems.URI_API_SAR, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetLanguage(), PrintStoreLocation.ROOT_PATH);
                richEditor.RunPrintTemplate(PrintTypeCodeStore.PRINT_TYPE_CODE__ChiTietGoiThau__MPS000119, DelegateRunPrinter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool DelegateRunPrinter(string printTypeCode, string fileName)
        {
            CommonParam param = new CommonParam();
            bool result = true;
            try
            {
                if (bidPrint != null)
                {
                    WaitingManager.Show();
                    var dataPrint = new MOS.EFMODEL.DataModels.HIS_BID();
                    MOS.Filter.HisBidViewFilter filter = new MOS.Filter.HisBidViewFilter();
                    filter.ID = bidPrint.ID;
                    var apiresult = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_BID>>(ApiConsumer.HisRequestUriStore.HIS_BID_GET, ApiConsumer.ApiConsumers.MosConsumer, filter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, param);
                    dataPrint = apiresult.FirstOrDefault();
                    MPS.Processor.Mps000119.PDO.Mps000119PDO Mps000119PDO = new MPS.Processor.Mps000119.PDO.Mps000119PDO(dataPrint, loadDataPrint(bidPrint));
                    MPS.ProcessorBase.Core.PrintData PrintData = null;
                    WaitingManager.Hide();

                    string printerName = "";

                    if (GlobalVariables.dicPrinter.ContainsKey(printTypeCode))
                    {
                        printerName = GlobalVariables.dicPrinter[printTypeCode];
                    }

                    if (ConfigApplications.CheDoInChoCacChucNangTrongPhanMem == 2)
                    {
                        PrintData = new MPS.ProcessorBase.Core.PrintData(printTypeCode, fileName, Mps000119PDO, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, printerName);
                    }
                    else
                    {
                        PrintData = new MPS.ProcessorBase.Core.PrintData(printTypeCode, fileName, Mps000119PDO, MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog, printerName);
                    }
                    MPS.MpsPrinter.Run(PrintData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                WaitingManager.Hide();
            }
            return result;
        }

        private List<MPS.Processor.Mps000119.PDO.HisBidMetyADO> loadDataPrint(MOS.EFMODEL.DataModels.HIS_BID bidModel)
        {
            CommonParam param = new CommonParam();
            List<MPS.Processor.Mps000119.PDO.HisBidMetyADO> bidMetyAdos = new List<MPS.Processor.Mps000119.PDO.HisBidMetyADO>();
            try
            {
                if (bidModel == null)
                {
                    return null;
                }
                foreach (var item in bidModel.HIS_BID_MEDICINE_TYPE)
                {
                    MPS.Processor.Mps000119.PDO.HisBidMetyADO bidMetyAdo = new MPS.Processor.Mps000119.PDO.HisBidMetyADO();
                    MOS.Filter.HisBidMedicineTypeViewFilter bidMetyFiter = new MOS.Filter.HisBidMedicineTypeViewFilter();
                    bidMetyFiter.ID = item.ID;

                    var bidMety = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.V_HIS_BID_MEDICINE_TYPE>>(ApiConsumer.HisRequestUriStore.HIS_BID_MEDICINE_TYPE_GETVIEW, ApiConsumer.ApiConsumers.MosConsumer, bidMetyFiter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, param);
                    if (bidMety != null && bidMety.Count > 0)
                    {
                        var atpMT = bidMety.FirstOrDefault();
                        Inventec.Common.Mapper.DataObjectMapper.Map<MPS.ADO.HisBidMetyADO>(bidMetyAdo, atpMT);
                        bidMetyAdo.TypeName = Inventec.Common.Resource.Get.Value(
                            "IVT_LANGUAGE_KEY__UC_HIS_BID_CREATE__MEDICINE",
                            Resources.ResourceLanguageManager.LanguageUCBidCreate,
                            cultureLang);
                        bidMetyAdo.TotalMoney = (bidMetyAdo.AMOUNT) * (bidMetyAdo.IMP_PRICE ?? 0) * ((bidMetyAdo.IMP_VAT_RATIO ?? 0) + 1);
                        bidMetyAdos.Add(bidMetyAdo);
                    }
                }
                foreach (var item in bidModel.HIS_BID_MATERIAL_TYPE)
                {
                    MPS.Processor.Mps000119.PDO.HisBidMetyADO bidMetyAdo = new MPS.Processor.Mps000119.PDO.HisBidMetyADO();
                    MOS.Filter.HisBidMaterialTypeViewFilter bidMatyFilter = new MOS.Filter.HisBidMaterialTypeViewFilter();
                    bidMatyFilter.ID = item.ID;
                    var bidMaty = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.V_HIS_BID_MATERIAL_TYPE>>(ApiConsumer.HisRequestUriStore.HIS_BID_MATERIAL_TYPE_GETVIEW, ApiConsumer.ApiConsumers.MosConsumer, bidMatyFilter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, param);
                    if (bidMaty != null && bidMaty.Count > 0)
                    {
                        var atpMT = bidMaty.FirstOrDefault();

                        Inventec.Common.Mapper.DataObjectMapper.Map<MPS.ADO.HisBidMetyADO>(bidMetyAdo, atpMT);
                        bidMetyAdo.MEDICINE_TYPE_CODE = atpMT.MATERIAL_TYPE_CODE;
                        bidMetyAdo.MEDICINE_TYPE_NAME = atpMT.MATERIAL_TYPE_NAME;
                        bidMetyAdo.TypeName = Inventec.Common.Resource.Get.Value(
                            "IVT_LANGUAGE_KEY__UC_HIS_BID_CREATE__MATERIAL",
                            Resources.ResourceLanguageManager.LanguageUCBidCreate,
                            cultureLang);
                        bidMetyAdo.TotalMoney = (bidMetyAdo.AMOUNT) * (bidMetyAdo.IMP_PRICE ?? 0) * ((bidMetyAdo.IMP_VAT_RATIO ?? 0) + 1);
                        bidMetyAdos.Add(bidMetyAdo);
                    }
                }
                foreach (var item in bidModel.HIS_BID_BLOOD_TYPE)
                {
                    MPS.Processor.Mps000119.PDO.HisBidMetyADO bidMetyAdo = new MPS.Processor.Mps000119.PDO.HisBidMetyADO();
                    MOS.Filter.HisBidBloodTypeViewFilter bidBltyFilter = new MOS.Filter.HisBidBloodTypeViewFilter();
                    bidBltyFilter.ID = item.ID;
                    var bidBlty = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.V_HIS_BID_BLOOD_TYPE>>(ApiConsumer.HisRequestUriStore.HIS_BID_BLOOD_TYPE_GETVIEW, ApiConsumer.ApiConsumers.MosConsumer, bidBltyFilter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, param);
                    if (bidBlty != null && bidBlty.Count > 0)
                    {
                        var atpMT = bidBlty.FirstOrDefault();
                        Inventec.Common.Mapper.DataObjectMapper.Map<MPS.ADO.HisBidMetyADO>(bidMetyAdo, atpMT);
                        bidMetyAdo.MEDICINE_TYPE_CODE = atpMT.BLOOD_TYPE_CODE;
                        bidMetyAdo.MEDICINE_TYPE_NAME = atpMT.BLOOD_TYPE_NAME;
                        bidMetyAdo.TypeName = Inventec.Common.Resource.Get.Value(
                            "IVT_LANGUAGE_KEY__UC_HIS_BID_CREATE__BLOOD",
                            Resources.ResourceLanguageManager.LanguageUCBidCreate,
                            cultureLang);
                        bidMetyAdo.TotalMoney = bidMetyAdo.AMOUNT * (bidMetyAdo.IMP_PRICE ?? 0) * ((bidMetyAdo.IMP_VAT_RATIO ?? 0) + 1);
                        bidMetyAdos.Add(bidMetyAdo);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
            return bidMetyAdos;
        }
    }
}
