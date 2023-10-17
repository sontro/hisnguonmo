using His.Bhyt.ExportXml;
using His.Bhyt.ExportXml.Base;
using Inventec.Common.Logging;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBaby;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class AutoExportXml2076Processor : BusinessBase
    {

        public void Run(long treatmentId)
        {
            try
            {
                if (HisHeinApprovalCFG.IS_AUTO_EXPORT_XML
                    && !string.IsNullOrWhiteSpace(HisTreatmentCFG.XML2076_FOLDER_PATH))
                {
                    V_HIS_TREATMENT_10 treatment = new HisTreatmentGet().GetView10ById(treatmentId);
                    if (treatment == null || treatment.IS_PAUSE != Constant.IS_TRUE)
                    {
                        return;
                    }

                    if (!((treatment.IS_HAS_BABY.HasValue && treatment.IS_HAS_BABY.Value == MOS.UTILITY.Constant.IS_TRUE) || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY || treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI || treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM))
                    {
                        return;
                    }
                    HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == treatment.BRANCH_ID);
                    List<V_HIS_BABY> babys = null;

                    string folderPath = string.Format("{0}\\{1}", HisTreatmentCFG.XML2076_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);

                    if (treatment.IS_HAS_BABY.HasValue && (long)treatment.IS_HAS_BABY.Value == Constant.IS_TRUE)
                    {
                        babys = new HisBabyGet().GetViewByTreatmentId(treatment.ID);
                    }

                    InputADO ado = new InputADO();
                    ado.AcsUsers = AcsUserCFG.DATA;
                    ado.Babys = babys;
                    ado.Branch = branch;
                    ado.Treatment2076 = treatment;
                    ado.Employees = HisEmployeeCFG.DATA;

                    CreateXmlMain xmlCreator = new CreateXmlMain(ado);
                    string messageError = "";
                    MemoryStream memoryStream = xmlCreator.Run2076Plus(ref messageError);

                    string sql = "UPDATE HIS_TREATMENT SET XML2076_URL = '{0}', XML2076_DESC= '{1}' WHERE ID = {2}";
                    string query = "";
                    if (memoryStream == null)
                    {
                        LogSystem.Error("Tu dong xuat XML2076 that bai");
                        query = String.Format(sql, "", messageError, treatmentId);
                    }
                    else
                    {
                        var fileName = string.Format("{0}___{1}___{2}.xml", Inventec.Common.DateTime.Get.Now().Value, treatment.TREATMENT_CODE, treatment.TDL_PATIENT_CODE);

                        FileUploadInfo fileUploadInfo = null;
                        try
                        {
                            fileUploadInfo = FileUpload.UploadFile(Constant.APPLICATION_CODE, folderPath, memoryStream, fileName, true);
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }

                        if (fileUploadInfo == null)
                        {
                            LogSystem.Error("Tai file XML2076 len he thong FSS that bai");
                            query = String.Format(sql, fileUploadInfo.Url, MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_UploadXml2076ThatBai, param.LanguageCode), treatmentId);
                        }
                        else
                        {
                            query = String.Format(sql, fileUploadInfo.Url, "", treatmentId);
                            LogSystem.Info("Xuat xml2076 ho so dieu tri: " + treatment.TREATMENT_CODE + " thanh cong");
                        }
                    }

                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Error("Cap nhat XML2076 URL cho HIS_TREATMENT that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

    }
}
