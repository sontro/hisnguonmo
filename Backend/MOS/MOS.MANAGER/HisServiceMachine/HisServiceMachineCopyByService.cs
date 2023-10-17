using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceMachine
{
    class HisServiceMachineCopyByService : BusinessBase
    {
        internal HisServiceMachineCopyByService()
            : base()
        {

        }

        internal HisServiceMachineCopyByService(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceMachineCopyByServiceSDO data, ref List<HIS_SERVICE_MACHINE> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyServiceId);
                valid = valid && IsGreaterThanZero(data.PasteServiceId);
                if (valid)
                {
                    List<HIS_SERVICE_MACHINE> newMestMatys = new List<HIS_SERVICE_MACHINE>();

                    List<HIS_SERVICE_MACHINE> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_MACHINE>("SELECT * FROM HIS_SERVICE_MACHINE WHERE SERVICE_ID = :param1", data.CopyServiceId);
                    List<HIS_SERVICE_MACHINE> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_MACHINE>("SELECT * FROM HIS_SERVICE_MACHINE WHERE SERVICE_ID = :param1", data.PasteServiceId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        V_HIS_SERVICE stock = Config.HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.CopyServiceId);
                        string name = stock != null ? stock.SERVICE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_DichVuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceMachines");
                    }

                    foreach (HIS_SERVICE_MACHINE copyData in copyMestMatys)
                    {
                        HIS_SERVICE_MACHINE mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.MACHINE_ID
                            == copyData.MACHINE_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SERVICE_MACHINE();
                            mestMaty.SERVICE_ID = data.PasteServiceId;
                            mestMaty.MACHINE_ID = copyData.MACHINE_ID;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisServiceMachineDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_SERVICE_MACHINE");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SERVICE_MACHINE>();
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        resultData.AddRange(newMestMatys);
                    }
                    if (IsNotNullOrEmpty(pasteMestMatys))
                    {
                        resultData.AddRange(pasteMestMatys);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                resultData = null;
            }
            return result;
        }
    }
}
