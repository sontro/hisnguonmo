using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    public partial class UCServiceExecute : UserControlBase
    {
        private void ValidNumberOfFilm()
        {
            try
            {
                string key = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(ServiceExecuteCFG.NumberOfFilmCFG);//Giá trị = 1 bắt buộc nhập số phim

                if (ServiceReqConstruct.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA && key.Trim() == "1")
                {
                    lciMunberOfFilm.AppearanceItemCaption.ForeColor = Color.Maroon;
                    Validation.FilmValidationRule FilmRule = new Validation.FilmValidationRule();
                    FilmRule.txtNumberOfFilm = txtNumberOfFilm;
                    FilmRule.ErrorText = ResourceMessage.TruongDuLieuBatBuoc;
                    FilmRule.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    this.dxValidationProvider1.SetValidationRule(txtNumberOfFilm, FilmRule);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidBeginTime()
        {
            try
            {
                Validation.BeginTimeValidationRule valid = new Validation.BeginTimeValidationRule();
                valid.dtBeginTime = dtBeginTime;
                valid.dtEndTime = dtEndTime;
                valid.IntructionTime = ServiceReqConstruct.INTRUCTION_TIME;
                valid.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtBeginTime, valid);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidEndTime()
        {
            try
            {
                Validation.EndTimeValidationRule valid = new Validation.EndTimeValidationRule();
                valid.dtEndTime = dtEndTime;
                valid.IntructionTime = ServiceReqConstruct.INTRUCTION_TIME;
                valid.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtEndTime, valid);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
