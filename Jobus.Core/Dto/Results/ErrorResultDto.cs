namespace Jobus.Core.Dto.Results
{
    public class ErrorResultDto : ResultDto
    {
        public ErrorDto Error { get; set; } = new ErrorDto();
    }
}
