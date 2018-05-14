using Jobus.Common.Results;
using System.Threading.Tasks;

namespace Jobus.Core.Services.Validations
{
    public interface IValidationService : IService
    {
        Task<Result> ValidateAsync<T>(T entity);
    }
}
