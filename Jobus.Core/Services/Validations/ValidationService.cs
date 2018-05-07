using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobus.Common.Results;

namespace Jobus.Core.Services.Validations
{
    public class ValidationService : IValidationService
    {
        public Task<Result> ValidateAsync<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
