using MediatR;
using MOS.Base.Response;
using MOS.Schema;


namespace MOS.Business.Cqrs;

//Expense CQRS For Personal
public record CreateExpenseCommand(int PersonalNumber,PersonalExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(int PersonalNumber,int ExpenseId,PersonalExpenseRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int PersonalNumber,int ExpenseId) : IRequest<ApiResponse>;

public record GetAllOwnExpenseQuery(int PersonalNumber) : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetOwnExpenseByIdQuery(int PersonalNumber,int ExpenseId) : IRequest<ApiResponse<ExpenseResponse>>;

//Bu query'i kullamıyorum onun yerine 'GetExpenseByParameterQuery' aynı işi yapmakta 
//public record GetOwnExpenseByParameterQuery(int PersonalNumber,string? ExpenseName,int? ApprovalStatus) : IRequest<ApiResponse<List<ExpenseResponse>>>;

//Expense CQRS For Admin

public record GetAllExpenseQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int ExpenseId) : IRequest<ApiResponse<ExpenseResponse>>;
public record GetExpenseByParameterQuery(string? ExpenseName,int? PersonalNumber, int? ApprovalStatus ,int? Min, int? Max, DateTime? afterdate,  DateTime? beforedate ) : IRequest<ApiResponse<List<ExpenseResponse>>>;


public record ApproveByIdCommand(int AdminNumber, int ExpenseId,AdminExpenseRequest Model) : IRequest<ApiResponse>;
public record RejectedByIdCommand(int AdminNumber,int ExpenseId,AdminExpenseRequest Model) : IRequest<ApiResponse>;


