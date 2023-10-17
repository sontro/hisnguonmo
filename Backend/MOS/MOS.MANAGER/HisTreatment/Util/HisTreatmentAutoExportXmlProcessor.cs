using AutoMapper;
using His.Bhyt.ExportXml;
using His.Bhyt.ExportXml.Base;
using Inventec.Common.Logging;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    class HisTreatmentAutoExportXmlProcessor : BusinessBase
    {
        public void Run(HIS_TREATMENT treatment, HIS_BRANCH branch, HIS_PATIENT_TYPE_ALTER pta, HIS_SERVICE_REQ exam)
        {
            try
            {
                if (treatment.IS_EMERGENCY != Constant.IS_TRUE && treatment.XML_CHECKIN_URL == null
                    && HisTreatmentCFG.AUTO_EXPORT_XML_CHECK_IN && !string.IsNullOrWhiteSpace(HisTreatmentCFG.XML_CHECK_IN_FOLDER_PATH))
                {
                    string folderPath = Path.Combine(HisTreatmentCFG.XML_CHECK_IN_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                    InputADO ado = new InputADO();
                    ado.Treatment = new HisTreatmentGet().GetView3ById(treatment.ID);
                    ado.Branch = branch;
                    ado.ExamServiceReq = exam;
                    ado.TotalIcdData = new HisIcdGet().Get(new HisIcdFilterQuery());
                    ado.ListSereServ = new HisSereServGet().GetView2ByTreatmentId(treatment.ID);

                    Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, V_HIS_PATIENT_TYPE_ALTER>();
                    ado.PatientTypeAlter = Mapper.Map<V_HIS_PATIENT_TYPE_ALTER>(pta);

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
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
