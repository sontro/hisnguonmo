using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using LIS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.CodeGenerator.HisServiceReq;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.GenerateBarcode
{
    class GenerateBarcodeProcessor : BusinessBase
    {
        internal GenerateBarcodeProcessor()
            : base()
        {

        }

        internal GenerateBarcodeProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_SERVICE_REQ> serviceReqs, HIS_TREATMENT treatment)
        {
            bool result = false;
            try
            {
                if (!Lis2CFG.IS_CALL_GENERATE_BARCODE && HisServiceReqCFG.GENERATE_BARCODE_OPTION != HisServiceReqCFG.GenrateBarcodeOption.DAY_WITH_NUMBER) return true;

                if (!Lis2CFG.IS_CALL_GENERATE_BARCODE && HisServiceReqCFG.GENERATE_BARCODE_OPTION == HisServiceReqCFG.GenrateBarcodeOption.DAY_WITH_NUMBER)
                {
                    List<HIS_SERVICE_REQ> requests = serviceReqs != null ? serviceReqs.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN).ToList() : null;
                    if (!IsNotNullOrEmpty(requests))
                    {
                        return true;
                    }

                    foreach (var req in requests)
                    {
                        req.BARCODE_TEMP = BarcodeGenerator.GetNext(Inventec.Common.DateTime.Get.Now() ?? 0);
                    }

                    result = true;
                }
                else if ((Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V1 && LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                    || (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2 && Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.INVENTEC))
                {
                    List<HIS_SERVICE_REQ> requests = serviceReqs != null ? serviceReqs.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN).ToList() : null;
                    if (!IsNotNullOrEmpty(requests))
                    {
                        return true;
                    }

                    Dictionary<string, List<long>> dicExecuteRoom = new Dictionary<string, List<long>>();
                    if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V1 && LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                    {
                        dicExecuteRoom = LisCFG.DIC_EXECUTE_ROOM;
                    }
                    else
                    {
                        dicExecuteRoom = LisInventecCFG.DIC_EXECUTE_ROOM;
                    }
                    if (dicExecuteRoom == null || dicExecuteRoom.Count == 0)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ChuaCauHinhDiaChiKetNoiLis);
                        throw new Exception("Co cau hinh goi sang lis sinh barcode nhung khong co cau hinh dia chi lis." + LogUtil.TraceData("dicExecuteRoom", dicExecuteRoom));
                    }
                    foreach (var dic in dicExecuteRoom)
                    {
                        var reqs = requests.Where(o => dic.Value != null && dic.Value.Contains(o.EXECUTE_ROOM_ID)).ToList();
                        if (IsNotNullOrEmpty(reqs))
                        {
                            Dictionary<string, HIS_SERVICE_REQ> dicReq = new Dictionary<string, HIS_SERVICE_REQ>();
                            List<GenerateBarcodeSDO> sdos = new List<GenerateBarcodeSDO>();
                            int count = 1;
                            foreach (var req in reqs)
                            {
                                GenerateBarcodeSDO sdo = new GenerateBarcodeSDO();
                                var executeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == req.EXECUTE_ROOM_ID);
                                sdo.ExecuteRoomCode = executeRoom.EXECUTE_ROOM_CODE;
                                sdo.ServiceReqCode = count.ToString(); ;
                                sdo.TreatmentCode = treatment.TREATMENT_CODE;
                                sdos.Add(sdo);
                                dicReq[count.ToString()] = req;
                                count++;
                            }

                            string tokenCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode();
                            ApiConsumer serviceConsumer = new ApiConsumer(dic.Key, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                            var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<List<GenerateBarcodeSDO>>>("/api/LisBarcode/Generate", null, sdos);

                            if (ro == null || !ro.Success || ro.Data == null || ro.Data.Count != sdos.Count)
                            {
                                LogSystem.Error("Gui y/c sinh barcode xet nghiem sang he thong LIS that bai. Ket qua: " + LogUtil.TraceData("ro", ro));
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_SinhBarcodeXetNghiemThatBai);
                                return false;
                            }

                            foreach (var rs in ro.Data)
                            {
                                var req = dicReq[rs.ServiceReqCode];
                                req.BARCODE_TEMP = rs.Barcode;
                                req.IS_SEND_BARCODE_TO_LIS = Constant.IS_TRUE;
                            }
                        }
                    }
                    result = true;
                }
                //Tich hop voi labconn, vietba ko ho tro
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }

}
