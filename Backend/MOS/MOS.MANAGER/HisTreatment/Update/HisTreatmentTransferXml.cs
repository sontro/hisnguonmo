using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment.Lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment
{
    class HisTreatmentTransferXml : BusinessBase
    {
        internal HisTreatmentTransferXml()
            : base()
        {

        }

        internal HisTreatmentTransferXml(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Gui thong tin ho so sang he thong cu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(List<long> ids)
        {
            bool result = true;
            try
            {
                bool valid = true;
                HisTreatmentCheck check = new HisTreatmentCheck(param);
                HisTreatmentLockSendDataXml sendData = new HisTreatmentLockSendDataXml();

                List<HIS_TREATMENT> listData = new List<HIS_TREATMENT>();
                valid = valid && IsNotNullOrEmpty(ids);
                valid = valid && check.VerifyIds(ids, listData);
                if (valid)
                {
                    List<string> errorTreatments = new List<string>();
                    foreach (var treatment in listData)
                    {
                        if (!sendData.Run(treatment))
                        {
                            errorTreatments.Add(treatment.TREATMENT_CODE);
                        }
                    }

                    if (IsNotNullOrEmpty(errorTreatments))
                    {
                        string codeStr = string.Join(",", errorTreatments);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDongBoThatBai, codeStr);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
