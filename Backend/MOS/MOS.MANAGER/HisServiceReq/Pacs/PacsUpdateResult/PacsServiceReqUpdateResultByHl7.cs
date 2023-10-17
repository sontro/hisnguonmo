using Inventec.Core;
using MedilinkHL7.SDK;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsUpdateResult
{
    class PacsServiceReqUpdateResultByHl7 : BusinessBase
    {
        private PacsServiceReqUpdateResult serviceReqUpdateResult;

        internal PacsServiceReqUpdateResultByHl7()
            : base()
        {
            this.Init();
        }

        internal PacsServiceReqUpdateResultByHl7(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.serviceReqUpdateResult = new PacsServiceReqUpdateResult(param);
        }

        internal TDO.PacsHl7TDO Run(TDO.PacsHl7TDO data)
        {
            TDO.PacsHl7TDO result = new TDO.PacsHl7TDO();
            string error = null;
            try
            {
                HL7ResultData receivedData = SendSANCY.ProcessReceivedHl7(data.message, PacsCFG.LIBRARY_HL7_VERSION);
                if (IsNotNull(receivedData) && IsNotNull(receivedData.SereServId))
                {
                    HisPacsResultTDO pacsResult = new HisPacsResultTDO();
                    pacsResult.AccessionNumber = receivedData.SereServId.ToString();
                    pacsResult.BeginTime = receivedData.BeginTime;
                    pacsResult.Conclude = receivedData.Conclude;
                    pacsResult.Description = receivedData.Description;
                    pacsResult.EndTime = receivedData.EndTime;
                    pacsResult.ExecuteLoginname = receivedData.ExecuteLoginname;
                    pacsResult.ExecuteUsername = receivedData.ExecuteUsername;
                    pacsResult.MachineCode = receivedData.Machine;
                    pacsResult.Note = receivedData.Note;
                    pacsResult.NumberOfFilm = receivedData.NumberOfFilm;
                    pacsResult.TechnicianLoginname = receivedData.SubclinicalResultLoginname;
                    pacsResult.TechnicianUsername = receivedData.SubclinicalResultUsername;
                    pacsResult.IsCancel = receivedData.IsCancel;

                    if (!serviceReqUpdateResult.Run(pacsResult))
                    {
                        throw new Exception("Cập nhật kết quả thất bại");
                    }
                }
                else
                {
                    error = "Lỗi phân tích dữ liệu HL7";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                error = ex.Message;
            }
            finally
            {
                string Message = param.GetMessage();
                string bugcode = param.GetBugCode();
                //param có thông tin thì thay thông báo lỗi bằng thông báo trong param
                if (!String.IsNullOrWhiteSpace(Message) || !String.IsNullOrWhiteSpace(bugcode))
                {
                    error = string.Format("{0} - {1}", bugcode, Message);
                }

                result.message = new AckMessage(null).CreateACK(data.message, error);
            }

            return result;
        }
    }
}
