using MediatR;
using MOS.Base.Response;
using MOS.Schema;


namespace MOS.Business.Cqrs;

//Payment CQRS
public record CreatePaymentCommand(PaymentRequest Model) : IRequest<ApiResponse<PaymentResponse>>;
public record UpdatePaymentCommand(int Id,PaymentRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllPaymentQuery() : IRequest<ApiResponse<List<PaymentResponse>>>;
public record GetPaymentByIdQuery(int Id) : IRequest<ApiResponse<PaymentResponse>>;
public record GetPaymentByParameterQuery(string IBAN,int? Min, int? Max) : IRequest<ApiResponse<List<PaymentResponse>>>;

