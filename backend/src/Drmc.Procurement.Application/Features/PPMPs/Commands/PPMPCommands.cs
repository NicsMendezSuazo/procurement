using Drmc.Procurement.Application.Features.PPMPs.DTO;
using Drmc.Procurement.Application.Features.PPMPs.Interfaces;
using MediatR;

namespace Drmc.Procurement.Application.Features.PPMPs.Commands;

public sealed record CreatePPMPCommand(
    CreatePPMPRequest Request,
    int CreatedBy,
    int DepartmentId) : IRequest<PPMPDetailDto>;

public sealed class CreatePPMPCommandHandler
    : IRequestHandler<CreatePPMPCommand, PPMPDetailDto>
{
    private readonly IPPMPService _service;

    public CreatePPMPCommandHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto> Handle(
        CreatePPMPCommand request,
        CancellationToken cancellationToken)
    {
        return _service.CreateAsync(
            request.Request,
            request.CreatedBy,
            request.DepartmentId);
    }
}

public sealed record SubmitPPMPCommand(
    int Id,
    int UserId,
    int DepartmentId) : IRequest<PPMPDetailDto>;

public sealed class SubmitPPMPCommandHandler
    : IRequestHandler<SubmitPPMPCommand, PPMPDetailDto>
{
    private readonly IPPMPService _service;

    public SubmitPPMPCommandHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto> Handle(
        SubmitPPMPCommand request,
        CancellationToken cancellationToken)
    {
        return _service.SubmitAsync(
            request.Id,
            request.UserId,
            request.DepartmentId);
    }
}

public sealed record ApprovePPMPCommand(
    int Id,
    ApprovePPMPRequest Request,
    int ApprovedBy,
    int DepartmentId) : IRequest<PPMPDetailDto>;

public sealed class ApprovePPMPCommandHandler
    : IRequestHandler<ApprovePPMPCommand, PPMPDetailDto>
{
    private readonly IPPMPService _service;

    public ApprovePPMPCommandHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto> Handle(
        ApprovePPMPCommand request,
        CancellationToken cancellationToken)
    {
        return _service.ApproveAsync(
            request.Id,
            request.Request,
            request.ApprovedBy,
            request.DepartmentId);
    }
}

public sealed record StartPPMPReviewCommand(
    int Id) : IRequest<PPMPDetailDto>;

public sealed class StartPPMPReviewCommandHandler
    : IRequestHandler<StartPPMPReviewCommand, PPMPDetailDto>
{
    private readonly IPPMPService _service;

    public StartPPMPReviewCommandHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto> Handle(
        StartPPMPReviewCommand request,
        CancellationToken cancellationToken)
    {
        return _service.StartReviewAsync(request.Id);
    }
}

public sealed record CompletePPMPReviewCommand(
    int Id,
    int ReviewedBy) : IRequest<PPMPDetailDto>;

public sealed class CompletePPMPReviewCommandHandler
    : IRequestHandler<CompletePPMPReviewCommand, PPMPDetailDto>
{
    private readonly IPPMPService _service;

    public CompletePPMPReviewCommandHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto> Handle(
        CompletePPMPReviewCommand request,
        CancellationToken cancellationToken)
    {
        return _service.CompleteReviewAsync(
            request.Id,
            request.ReviewedBy);
    }
}

public sealed record UpdatePPMPCommand(
    int Id,
    UpdatePPMPRequest Request,
    int ChangedBy) : IRequest<PPMPDetailDto>;

public sealed class UpdatePPMPCommandHandler
    : IRequestHandler<UpdatePPMPCommand, PPMPDetailDto>
{
    private readonly IPPMPService _service;

    public UpdatePPMPCommandHandler(IPPMPService service)
    {
        _service = service;
    }

    public Task<PPMPDetailDto> Handle(
        UpdatePPMPCommand request,
        CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(
            request.Id,
            request.Request,
            request.ChangedBy);
    }
}
