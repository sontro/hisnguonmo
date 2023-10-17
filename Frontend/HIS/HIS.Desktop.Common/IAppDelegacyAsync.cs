using System.Threading.Tasks;
namespace HIS.Desktop.Common
{
    public interface IAppDelegacyAsync
    {
        Task<object> Execute();
    }
}
