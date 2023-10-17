using MOS.EFMODEL.DataModels;
using MOS.SDO;
namespace HIS.Desktop.Plugins.RegisterV3.Register
{
    interface IServiceRequestRegisterPatientProfile
    {
        HisPatientProfileSDO Run();
    }
}
