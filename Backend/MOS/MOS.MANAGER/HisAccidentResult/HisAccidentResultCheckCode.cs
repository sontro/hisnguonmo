using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentResult
{
    /// <summary>
    /// Nguyen tac xay dung lop Checker: chu dong va doc lap
    /// 1. Chu dong: phai dung o goc do Checker la nguoi chu dong dua ra API check cho nguoi khac su dung tu khi ho con chua co nhu cau. Viec check cai gi, y nghia ra sao can duoc phan tich suy nghi khong le thuoc vao tinh huong cu the de tranh API bi han che ve mat tam nhin.
    /// 2. Doc lap: 1 ham check chi xu ly 1 va chi dung 1 viec. Tuyet doi tranh ham check nay lai phai lo ho nhiem vu cua 1 ham check khac. Nhu y tren, viec cua Checker la dua ra ham check & viec cua nguoi su dung la phai hieu ro ve cac ham check de su dung ket hop chung theo dung thu tu. Tu duy nay neu duoc trien khai tot se giup cac ham check khong bi phu thuoc vao nhau, co duoc su don gian, de tai su dung.
    /// </summary>
    partial class HisAccidentResultCheck : BusinessBase
    {
        /// <summary>
        /// Kiem tra ma da ton tai hay chua, id duoc su dung trong truong hop muon bo qua chinh ma cua minh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisAccidentResultDAO.ExistsCode(code, id))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
