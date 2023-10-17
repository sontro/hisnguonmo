using AutoMapper;
using His.Bhyt.ExportXml;
using His.Bhyt.ExportXml.Base;
using Inventec.Common.Logging;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Util
{
    class HisServiceReqAutoExportXmlProcessor : BusinessBase
    {
        public void Run(HIS_TREATMENT treatment)
        {
            try
            {
                if (treatment.XML_CHECKIN_URL == null)
                {
                    if (HisTreatmentCFG.AUTO_EXPORT_XML_CHECK_IN && !string.IsNullOrWhiteSpace(HisTreatmentCFG.XML_CHECK_IN_FOLDER_PATH))
                    {
                        HIS_BRANCH branch = new HisBranchGet().GetById(treatment.BRANCH_ID);
                        HIS_ICD icd = new HisIcdGet().GetByCode(treatment.ICD_CODE);
                        
                        string folderPath = Path.Combine(HisTreatmentCFG.XML_CHECK_IN_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                        InputADO ado = new InputADO();
                        if (icd != null)
                            ado.TotalIcdData.Add(icd);
                        ado.Treatment = new HisTreatmentGet().GetView3ById(treatment.ID);
                        ado.ListSereServ = new HisSereServGet().GetView2ByTreatmentId(treatment.ID);
                        ado.PatientTypeAlter = new HisPatientTypeAlterGet().GetViewLastByTreatmentId(treatment.ID);

                        CreateXmlMain xmlCreator = new CreateXmlMain(ado);

                        string fileName = "";
                        string messageError = "";
                        MemoryStream memoryStream = xmlCreator.RunCheckInPlus(ref messageError, ref fileName);

                        string sql = "UPDATE HIS_TREATMENT SET XML_CHECKIN_URL = '{0}', XML_CHECKIN_DESC = '{1}' WHERE ID = {2}";
                        string query = "";
                        if (memoryStream == null)
                        {
                            LogSystem.Error("Tu dong xuat XML check in that bai");
                            query = String.Format(sql, "", messageError, treatment.ID);
                            Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => ado), ado));
                        }
                        else
                        {
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
                                LogSystem.Error("Tai file XML check in len he thong FSS that bai");
                                query = String.Format(sql, "", MOS.MANAGER.Base.MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_UploadXml4210ThatBai, param.LanguageCode), treatment.ID);
                            }
                            else
                            {
                                query = String.Format(sql, fileUploadInfo.Url, "", treatment.ID);
                                LogSystem.Info("Xuất XML checkin ho so dieu tri: " + treatment.TREATMENT_CODE + " thanh cong");
                            }
                        }

                        if (!DAOWorker.SqlDAO.Execute(query))
                        {
                            LogSystem.Error("Cap nhat XML_CHECKIN_URL cho HIS_TREATMENT that bai");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
