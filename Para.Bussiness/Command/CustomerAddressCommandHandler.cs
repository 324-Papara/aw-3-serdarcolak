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

public class CustomerAddressCommandHandler :
    IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        //Fluent Validation
        CustomerAddressValidator validator = new CustomerAddressValidator();
        await validator.ValidateAndThrowAsync(request.Request);
        
        var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
        await unitOfWork.CustomerAddressRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerAddressResponse>(mapped);
        return new ApiResponse<CustomerAddressResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
        mapped.Id = request.CustomerId;
        await unitOfWork.CustomerAddressRepository.Update(request.CustomerId, mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CustomerAddressRepository.Delete(request.CustomerId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}