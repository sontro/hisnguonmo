using System.Threading.Tasks;
namespace HIS.Desktop.Common
{
    public interface IAppDelegacyTAsync
    {
        Task<T> Execute<T>();
    }
}
