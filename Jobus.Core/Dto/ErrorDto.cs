using System.Collections.Generic;

namespace Jobus.Core.Dto
{
    public class ErrorDto
    {
        public int ErrorsCount => Errors.Count;
        public List<StructDto<string, string>> Errors = new List<StructDto<string, string>>();
    }
}
