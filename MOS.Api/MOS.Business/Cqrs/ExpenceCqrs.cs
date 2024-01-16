using MediatR;
using MOS.Base.Response;
using MOS.Schema;


namespace MOS.Business.Cqrs;

//Expense CQRS For Personal
   //personal
    //create, update, delate, getbyid(kendine ait),  getallExpense(kendine ait), getparameter(kendine ait),
    //onaylananları getir// beklemede olanları getir // onaylanmayanları getir
public record CreateExpenseCommand(int PersonalNumber,PersonalExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(int PersonalNumber,int ExpenseId,PersonalExpenseRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int PersonalNumber,int ExpenseId) : IRequest<ApiResponse>;

public record GetAllOwnExpenseQuery(int PersonalNumber) : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetOwnExpenseByIdQuery(int PersonalNumber,int ExpenseId) : IRequest<ApiResponse<ExpenseResponse>>;
public record GetOwnExpenseByParameterQuery(int PersonalNumber,string ExpenseName,int ApprovalStatus) : IRequest<ApiResponse<List<ExpenseResponse>>>;

//Expense CQRS For Admin
        //getallExpense //getparameter //getbyid
        //onaylananları getir// beklemede olanları getir // onaylanmayanları getir
        //Onaylabyid // reddetbyid

public record GetAllExpenseQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int ExpenseId) : IRequest<ApiResponse<ExpenseResponse>>;
public record GetExpenseByParameterQuery(string ExpenseName,int ApprovalStatus) : IRequest<ApiResponse<List<ExpenseResponse>>>;


public record ApproveByIdCommand(int AdminNumber, int ExpenseId,AdminExpenceRequest Model) : IRequest<ApiResponse>;
public record RejectedByIdCommand(int AdminNumber,int ExpenseId,AdminExpenceRequest Model) : IRequest<ApiResponse>;


