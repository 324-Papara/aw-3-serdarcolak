using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Validation;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class CustomerPhoneCommandHandler:
    IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
    IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        //Fluent Validation
        CustomerPhoneValidator validator = new CustomerPhoneValidator();
        await validator.ValidateAndThrowAsync(request.Request);
        
        var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
        await unitOfWork.CustomerPhoneRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerPhoneResponse>(mapped);
        return new ApiResponse<CustomerPhoneResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
        mapped.Id = request.CustomerId;
        await unitOfWork.CustomerPhoneRepository.Update(request.CustomerId, mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CustomerPhoneRepository.Delete(request.CustomerId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}