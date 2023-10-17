using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceMaty
{
    class HisServiceMatyCopyByMaty : BusinessBase
    {
        private List<HIS_SERVICE_MATY> recentServiceMatys;

        internal HisServiceMatyCopyByMaty()
            : base()
        {

        }

        internal HisServiceMatyCopyByMaty(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceMatyCopyByMatySDO data, ref List<HIS_SERVICE_MATY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMaterialTypeId);
                valid = valid && IsGreaterThanZero(data.PasteMaterialTypeId);
                if (valid)
                {
                    List<HIS_SERVICE_MATY> newServiceMatys = new List<HIS_SERVICE_MATY>();
                    List<HIS_SERVICE_MATY> oldServiceMatys = new List<HIS_SERVICE_MATY>();
                    List<HIS_SERVICE_MATY> copyServiceMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_MATY>("SELECT * FROM HIS_SERVICE_MATY WHERE MATERIAL_TYPE_ID = :param1", data.CopyMaterialTypeId);
                    List<HIS_SERVICE_MATY> pasteServiceMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_MATY>("SELECT * FROM HIS_SERVICE_MATY WHERE MATERIAL_TYPE_ID = :param1", data.PasteMaterialTypeId);
                    if (!IsNotNullOrEmpty(copyServiceMatys))
                    {
                        HIS_MATERIAL_TYPE materialType = Config.HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMaterialTypeId);
                        string name = materialType != null ? materialType.MATERIAL_TYPE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterialType_VatTuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceMatys");
                    }

                    foreach (HIS_SERVICE_MATY copyData in copyServiceMatys)
                    {
                        HIS_SERVICE_MATY serviceMaty = pasteServiceMatys != null ? pasteServiceMatys.FirstOrDefault(o => o.SERVICE_ID == copyData.SERVICE_ID) : null;
                        if (serviceMaty != null)
                        {
                            serviceMaty.EXPEND_AMOUNT = copyData.EXPEND_AMOUNT;
                            serviceMaty.EXPEND_PRICE = copyData.EXPEND_PRICE;
                            oldServiceMatys.Add(serviceMaty);
                        }
                        else
                        {
                            serviceMaty = new HIS_SERVICE_MATY();
                            serviceMaty.MATERIAL_TYPE_ID = data.PasteMaterialTypeId;
                            serviceMaty.SERVICE_ID = copyData.SERVICE_ID;
                            serviceMaty.EXPEND_AMOUNT = copyData.EXPEND_AMOUNT;
                            serviceMaty.EXPEND_PRICE = copyData.EXPEND_PRICE;
                            serviceMaty.SERVICE_UNIT_ID = copyData.SERVICE_UNIT_ID;
                            newServiceMatys.Add(serviceMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newServiceMatys))
                    {
                        if (!DAOWorker.HisServiceMatyDAO.CreateList(newServiceMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_SERVICE_MATY");
                        }
                        this.recentServiceMatys = newServiceMatys;
                    }

                    if (IsNotNullOrEmpty(oldServiceMatys))
                    {
                        if (!DAOWorker.HisServiceMatyDAO.UpdateList(oldServiceMatys))
                        {
                            throw new Exception("Khong sua duoc HIS_SERVICE_MATY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SERVICE_MATY>();
                    if (IsNotNullOrEmpty(newServiceMatys))
                    {
                        resultData.AddRange(newServiceMatys);
                    }
                    if (IsNotNullOrEmpty(pasteServiceMatys))
                    {
                        resultData.AddRange(pasteServiceMatys);
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
                if (IsNotNullOrEmpty(this.recentServiceMatys))
                {
                    if (!DAOWorker.HisServiceMatyDAO.TruncateList(this.recentServiceMatys))
                    {
                        Logging("Rollback HIS_SERVICE_MATY that bai. Kiem tra lai du lieu", LogType.Warn);
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
