using Grpc.Core;

using Utils;

namespace UtilitiesGrpCService.Services;

public class DateUtilsService :DateUtils.DateUtilsBase
{
    private readonly Utils.DateUtilities _utils;

    public DateUtilsService()
    {
        _utils = new DateUtilities();
    }

    public override Task<DateUtilsResponse> isWeekday(DateUtilsRequest request, ServerCallContext context)
    {
        var incomingDate = request.Date.ToDateTime();
        var result = _utils.IsWeekDay(incomingDate);

        return Task.FromResult(new DateUtilsResponse { Ok = result });
        
    }

    public override Task<DateUtilsResponse> isWeekend(DateUtilsRequest request, ServerCallContext context)
    {
        var incomingDate = request.Date.ToDateTime();
        var result = _utils.IsWeekend(incomingDate);

        return Task.FromResult(new DateUtilsResponse { Ok = result });
    }
}
