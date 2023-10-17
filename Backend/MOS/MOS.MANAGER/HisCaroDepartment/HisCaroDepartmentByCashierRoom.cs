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
    class HisCaroDepartmentCopyByCashierRoom : BusinessBase
    {
        internal HisCaroDepartmentCopyByCashierRoom()
            : base()
        {

        }

        internal HisCaroDepartmentCopyByCashierRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisCaroDepaCopyByCashierRoomSDO data, ref List<HIS_CARO_DEPARTMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyCashierRoomId);
                valid = valid && IsGreaterThanZero(data.PasteCashierRoomId);
                if (valid)
                {
                    List<HIS_CARO_DEPARTMENT> newCaroDepartments = new List<HIS_CARO_DEPARTMENT>();

                    List<HIS_CARO_DEPARTMENT> copyCaroDepartments = DAOWorker.SqlDAO.GetSql<HIS_CARO_DEPARTMENT>("SELECT * FROM HIS_CARO_DEPARTMENT WHERE CASHIER_ROOM_ID = :param1", data.CopyCashierRoomId);
                    List<HIS_CARO_DEPARTMENT> pasteCaroDepartments = DAOWorker.SqlDAO.GetSql<HIS_CARO_DEPARTMENT>("SELECT * FROM HIS_CARO_DEPARTMENT WHERE CASHIER_ROOM_ID = :param1", data.PasteCashierRoomId);
                    if (!IsNotNullOrEmpty(copyCaroDepartments))
                    {
                        V_HIS_CASHIER_ROOM stock = Config.HisCashierRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyCashierRoomId);
                        string name = stock != null ? stock.CASHIER_ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyCaroDepartments");
                    }

                    foreach (HIS_CARO_DEPARTMENT copyData in copyCaroDepartments)
                    {
                        HIS_CARO_DEPARTMENT mestMaty = pasteCaroDepartments != null ? pasteCaroDepartments.FirstOrDefault(o => o.DEPARTMENT_ID == copyData.DEPARTMENT_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_CARO_DEPARTMENT();
                            mestMaty.CASHIER_ROOM_ID = data.PasteCashierRoomId;
                            mestMaty.DEPARTMENT_ID = copyData.DEPARTMENT_ID;
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
