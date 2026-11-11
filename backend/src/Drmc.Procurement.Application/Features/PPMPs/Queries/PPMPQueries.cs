using Drmc.Procurement.Application.Features.PPMPs.DTO;
using Drmc.Procurement.Application.Features.PPMPs.Interfaces;
using MediatR;

namespace Drmc.Procurement.Application.Features.PPMPs.Queries;

public sealed record GetPagedPPMPsQuery(
    int? DepartmentId,
    int Page,
    int PageSize,
    string[]? Statuses,
    int? PlanningYear) : IRequest<PagedResult<PPMPDto>>;

public sealed class GetPagedPPMPsQueryHandler
    : IRequestHandler<GetPagedPPMPsQuery, PagedResult<PPMPDto>>
{
    private readonly IPPMPService _service;

    public GetPagedPPMPsQueryHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PagedResult<PPMPDto>> Handle(
        GetPagedPPMPsQuery request,
        CancellationToken cancellationToken)
    {
        return _service.GetPagedAsync(
            request.DepartmentId,
            request.Page,
            request.PageSize,
            request.Statuses,
            request.PlanningYear);
    }
}

public sealed record GetPPMPSummaryQuery(
    int? DepartmentId) : IRequest<PPPMSummaryDto>;

public sealed class GetPPMPSummaryQueryHandler
    : IRequestHandler<GetPPMPSummaryQuery, PPPMSummaryDto>
{
    private readonly IPPMPService _service;

    public GetPPMPSummaryQueryHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPPMSummaryDto> Handle(
        GetPPMPSummaryQuery request,
        CancellationToken cancellationToken)
    {
        return _service.GetSummaryAsync(request.DepartmentId);
    }
}

public sealed record GetPPMPByIdQuery(
    int Id,
    int? DepartmentId) : IRequest<PPMPDetailDto?>;

public sealed class GetPPMPByIdQueryHandler
    : IRequestHandler<GetPPMPByIdQuery, PPMPDetailDto?>
{
    private readonly IPPMPService _service;

    public GetPPMPByIdQueryHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto?> Handle(
        GetPPMPByIdQuery request,
        CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id, request.DepartmentId);
    }
}

public sealed record GetPPMPChangeHistoryQuery(
    int Id,
    int? DepartmentId) : IRequest<List<PPMPChangeHistoryDto>>;

public sealed class GetPPMPChangeHistoryQueryHandler
    : IRequestHandler<
        GetPPMPChangeHistoryQuery,
        List<PPMPChangeHistoryDto>>
{
    private readonly IPPMPService _service;

    public GetPPMPChangeHistoryQueryHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<List<PPMPChangeHistoryDto>> Handle(
        GetPPMPChangeHistoryQuery request,
        CancellationToken cancellationToken)
    {
        return _service.GetChangeHistoryAsync(
            request.Id,
            request.DepartmentId);
    }
}
