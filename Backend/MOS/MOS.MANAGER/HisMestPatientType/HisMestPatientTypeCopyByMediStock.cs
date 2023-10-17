using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMestPatientType
{
    class HisMestPatientTypeCopyByMediStock : BusinessBase
    {
        private List<HIS_MEST_PATIENT_TYPE> recentMestPatientTypes;

        internal HisMestPatientTypeCopyByMediStock()
            : base()
        {

        }

        internal HisMestPatientTypeCopyByMediStock(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestPatientTypeCopyByMediStockSDO data, ref List<HIS_MEST_PATIENT_TYPE> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMediStockId);
                valid = valid && IsGreaterThanZero(data.PasteMediStockId);
                if (valid)
                {
                    List<HIS_MEST_PATIENT_TYPE> newMestPatientTypes = new List<HIS_MEST_PATIENT_TYPE>();
                    List<HIS_MEST_PATIENT_TYPE> oldMestPatientTypes = new List<HIS_MEST_PATIENT_TYPE>();
                    List<HIS_MEST_PATIENT_TYPE> copyMestPatientTypes = DAOWorker.SqlDAO.GetSql<HIS_MEST_PATIENT_TYPE>("SELECT * FROM HIS_MEST_PATIENT_TYPE WHERE MEDI_STOCK_ID = :param1", data.CopyMediStockId);
                    List<HIS_MEST_PATIENT_TYPE> pasteMestPatientTypes = DAOWorker.SqlDAO.GetSql<HIS_MEST_PATIENT_TYPE>("SELECT * FROM HIS_MEST_PATIENT_TYPE WHERE MEDI_STOCK_ID = :param1", data.PasteMediStockId);
                    if (!IsNotNullOrEmpty(copyMestPatientTypes))
                    {
                        V_HIS_MEDI_STOCK stock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMediStockId);
                        string name = stock != null ? stock.MEDI_STOCK_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_KhoChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestPatientTypes");
                    }

                    foreach (HIS_MEST_PATIENT_TYPE copyData in copyMestPatientTypes)
                    {
                        HIS_MEST_PATIENT_TYPE mestMety = pasteMestPatientTypes != null ? pasteMestPatientTypes.FirstOrDefault(o => o.PATIENT_TYPE_ID == copyData.PATIENT_TYPE_ID) : null;
                        if (mestMety == null)
                        {
                            mestMety = new HIS_MEST_PATIENT_TYPE();
                            mestMety.MEDI_STOCK_ID = data.PasteMediStockId;
                            mestMety.PATIENT_TYPE_ID = copyData.PATIENT_TYPE_ID;
                            newMestPatientTypes.Add(mestMety);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestPatientTypes))
                    {
                        if (!DAOWorker.HisMestPatientTypeDAO.CreateList(newMestPatientTypes))
                        {
                            throw new Exception("Khong tao duoc HIS_MEST_PATIENT_TYPE");
                        }
                        this.recentMestPatientTypes = newMestPatientTypes;
                    }

                    if (IsNotNullOrEmpty(oldMestPatientTypes))
                    {
                        if (!DAOWorker.HisMestPatientTypeDAO.UpdateList(oldMestPatientTypes))
                        {
                            throw new Exception("Khong sua duoc HIS_MEST_PATIENT_TYPE");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEST_PATIENT_TYPE>();
                    if (IsNotNullOrEmpty(newMestPatientTypes))
                    {
                        resultData.AddRange(newMestPatientTypes);
                    }
                    if (IsNotNullOrEmpty(pasteMestPatientTypes))
                    {
                        resultData.AddRange(pasteMestPatientTypes);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
                resultData = null;
            }
            return result;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMestPatientTypes))
                {
                    if (!DAOWorker.HisMestPatientTypeDAO.TruncateList(this.recentMestPatientTypes))
                    {
                        Logging("Rollback HIS_MEST_PATIENT_TYPE that bai. Kiem tra lai du lieu", LogType.Warn);
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
