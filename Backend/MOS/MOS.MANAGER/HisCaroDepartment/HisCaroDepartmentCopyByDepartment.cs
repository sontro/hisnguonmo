using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCaroDepartment
{
    class HisCaroDepartmentCopyByDepartment : BusinessBase
    {
        internal HisCaroDepartmentCopyByDepartment()
            : base()
        {

        }

        internal HisCaroDepartmentCopyByDepartment(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisCaroDepaCopyByDepartmentSDO data, ref List<HIS_CARO_DEPARTMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyDepartmentId);
                valid = valid && IsGreaterThanZero(data.PasteDepartmentId);
                if (valid)
                {
                    List<HIS_CARO_DEPARTMENT> newCaroDepartments = new List<HIS_CARO_DEPARTMENT>();

                    List<HIS_CARO_DEPARTMENT> copyCaroDepartments = DAOWorker.SqlDAO.GetSql<HIS_CARO_DEPARTMENT>("SELECT * FROM HIS_CARO_DEPARTMENT WHERE DEPARTMENT_ID = :param1", data.CopyDepartmentId);
                    List<HIS_CARO_DEPARTMENT> pasteCaroDepartments = DAOWorker.SqlDAO.GetSql<HIS_CARO_DEPARTMENT>("SELECT * FROM HIS_CARO_DEPARTMENT WHERE DEPARTMENT_ID = :param1", data.PasteDepartmentId);
                    if (!IsNotNullOrEmpty(copyCaroDepartments))
                    {
                        HIS_DEPARTMENT room = Config.HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == data.CopyDepartmentId);
                        string name = room != null ? room.DEPARTMENT_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartment_KhoaChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copySaroExros");
                    }

                    foreach (HIS_CARO_DEPARTMENT copyData in copyCaroDepartments)
                    {
                        HIS_CARO_DEPARTMENT mestMaty = pasteCaroDepartments != null ? pasteCaroDepartments.FirstOrDefault(o => o.CASHIER_ROOM_ID == copyData.CASHIER_ROOM_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_CARO_DEPARTMENT();
                            mestMaty.DEPARTMENT_ID = data.PasteDepartmentId;
                            mestMaty.CASHIER_ROOM_ID = copyData.CASHIER_ROOM_ID;
                            newCaroDepartments.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newCaroDepartments))
                    {
                        if (!DAOWorker.HisCaroDepartmentDAO.CreateList(newCaroDepartments))
                        {
                            throw new Exception("Khong tao duoc HIS_CARO_DEPARTMENT");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_CARO_DEPARTMENT>();
                    if (IsNotNullOrEmpty(newCaroDepartments))
                    {
                        resultData.AddRange(newCaroDepartments);
                    }
                    if (IsNotNullOrEmpty(pasteCaroDepartments))
                    {
                        resultData.AddRange(pasteCaroDepartments);
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
