using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Data;
using MOS.Data.Entity;
using MOS.Schema;

namespace MOS.Business.Query
{
    public class ReportQueryHandler : IRequestHandler<GetReportQuery, ApiResponse<ReportResponse>>
    {
        private readonly MosDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;

        public ReportQueryHandler(MosDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ApiResponse<ReportResponse>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            ReportResponse reportResponse = new ReportResponse
            {
                // Diğer özellikleri başlatın
                ReportEachPersonalList = new List<ReportEachPersonal>()
            };



            DateTime? StartExpenceDate = new DateTime(2024, 1, 1);
            DateTime? EndExpenceDate = DateTime.Now;
            DateTime? StartDecisionDate = new DateTime(2024, 1, 1);
            DateTime? EndDecisionDate = DateTime.Now;
            if (request.Model.StartExpenceDate != null)
            {
                StartExpenceDate = request.Model.StartExpenceDate;
            }
            if (request.Model.EndExpenceDate != null)
            {
                EndExpenceDate = request.Model.EndExpenceDate;
            }
            if (request.Model.StartDecisionDate != null)
            {
                StartDecisionDate = request.Model.StartDecisionDate;
            }
            if (request.Model.EndDecisionDate != null)
            {
                EndDecisionDate = request.Model.EndDecisionDate;
            }
            //string connectionString = "Server=DESKTOP-J6P579G\\SQLEXPRESS;Database=MasrafOdemeSistemi;trusted_connection=true;TrustServerCertificate=true;";
            string connectionString = _configuration.GetValue<string>("ConnectionStrings:MsSqlConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Dapper'ı kullanarak sorguyu çalıştırma
                if (request.Model.PersonalNumberList.Count == 0)
                {
                    request.Model.PersonalNumberList = connection.Query<int>("SELECT PersonalNumber FROM Personal").ToList();
                }
                foreach (int PNumber in request.Model.PersonalNumberList)
                {

                    ReportEachPersonal reportEachPersonal = new ReportEachPersonal();

                    //personal
                    string personalquery = @"SELECT *
                        FROM dbo.Personal
                        WHERE PersonalNumber = @PersonalNumber";
                    Personal personal = connection.Query<Personal>(personalquery, new { PersonalNumber = PNumber }).FirstOrDefault();
                    reportEachPersonal.PersonalResponse = mapper.Map<Personal, PersonalResponse>(personal);

                    //WaitingExpenseList
                    string query = @"SELECT *
                        FROM dbo.Expense
                        WHERE PersonalNumber = @PersonalNumber
                            AND (ExpenseCreateDate >= @StartExpenceDate AND ExpenseCreateDate <= @EndExpenceDate)
                            AND (DecisionDate IS NULL OR (DecisionDate >= @StartDecisionDate AND DecisionDate <= @EndDecisionDate))";

                    List<Expense> WaitingExpenseList = connection.Query<Expense>(query,
                     new
                     {
                         PersonalNumber = PNumber,
                         StartExpenceDate = StartExpenceDate,
                         EndExpenceDate = EndExpenceDate,
                         StartDecisionDate = StartDecisionDate,
                         EndDecisionDate = EndDecisionDate
                     }).ToList();
                    reportEachPersonal.WaitingExpenseList = mapper.Map<List<Expense>, List<ExpenseResponse>>(WaitingExpenseList);

                    foreach (var item in WaitingExpenseList)
                    {
                        reportEachPersonal.MoneySpentByPersonal += item.ExpenseAmount;
                        reportResponse.TotalMoneySpentByPersonals += item.ExpenseAmount;
                    }

                    //AproveedExpenseList
                    query = @"SELECT *
                        FROM dbo.Expense
                        WHERE PersonalNumber = @PersonalNumber
                            AND (ExpenseCreateDate >= @StartExpenceDate AND ExpenseCreateDate <= @EndExpenceDate)
                            AND (DecisionDate >= @StartDecisionDate AND DecisionDate <= @EndDecisionDate)
                            AND ApprovalStatus = 2";
                    List<Expense> AproveedExpenseList = connection.Query<Expense>(query,
                    new
                    {
                        PersonalNumber = PNumber,
                        StartExpenceDate = StartExpenceDate,
                        EndExpenceDate = EndExpenceDate,
                        StartDecisionDate = StartDecisionDate,
                        EndDecisionDate = EndDecisionDate
                    }).ToList();
                    reportEachPersonal.AproveedExpenseList = mapper.Map<List<Expense>, List<ExpenseResponse>>(AproveedExpenseList);
                    foreach (var item in AproveedExpenseList)
                    {
                        reportEachPersonal.ApprovedSpentMoney += item.ExpenseAmount;
                        reportResponse.TotalMoneySpentByPersonals += item.ExpenseAmount;
                        reportResponse.TotalApprovedSpentMoney += item.ExpenseAmount;
                    }
                    //RejecetExpenseList
                    query = @"SELECT *
                        FROM dbo.Expense
                        WHERE PersonalNumber = @PersonalNumber
                            AND (ExpenseCreateDate >= @StartExpenceDate AND ExpenseCreateDate <= @EndExpenceDate)
                            AND (DecisionDate >= @StartDecisionDate AND DecisionDate <= @EndDecisionDate)
                            AND ApprovalStatus = 3";
                    List<Expense> RejecetExpenseList = connection.Query<Expense>(query,
                    new
                    {
                        PersonalNumber = PNumber,
                        StartExpenceDate = StartExpenceDate,
                        EndExpenceDate = EndExpenceDate,
                        StartDecisionDate = StartDecisionDate,
                        EndDecisionDate = EndDecisionDate
                    }).ToList();
                    reportEachPersonal.RejecetExpenseList = mapper.Map<List<Expense>, List<ExpenseResponse>>(RejecetExpenseList);
                    foreach (var item in RejecetExpenseList)
                    {
                        reportEachPersonal.RejectedSpentMoney += item.ExpenseAmount;
                        reportResponse.TotalMoneySpentByPersonals += item.ExpenseAmount;
                        reportResponse.TotalRejectedSpentMoney += item.ExpenseAmount;
                    }

                    //PaymentExpenseList
                    query = @"SELECT p.*
                        FROM dbo.Payment p
                        INNER JOIN dbo.Expense e ON p.ExpenseId = e.ExpenseId
                        WHERE e.PersonalNumber = @PersonalNumber
                            AND (e.ExpenseCreateDate >= @StartExpenceDate AND e.ExpenseCreateDate <= @EndExpenceDate)
                            AND (e.DecisionDate IS NULL OR (e.DecisionDate >= @StartDecisionDate AND e.DecisionDate <= @EndDecisionDate))
                            AND e.ApprovalStatus = 2";

                    List<Payment> PaymentList = connection.Query<Payment>(query,
                     new
                     {
                         PersonalNumber = PNumber,
                         StartExpenceDate = StartExpenceDate,
                         EndExpenceDate = EndExpenceDate,
                         StartDecisionDate = StartDecisionDate,
                         EndDecisionDate = EndDecisionDate
                     }).ToList();
                    reportEachPersonal.PaymentList = mapper.Map<List<Payment>, List<PaymentResponse>>(PaymentList);

                    // Personali Rapora ekle

                    reportResponse.ReportEachPersonalList.Add(reportEachPersonal);
                }

            }
            reportResponse.ReportCrateDate = DateTime.Now;
            reportResponse.StartTheDate = (DateTime)StartExpenceDate;
            reportResponse.EndTheDate = (DateTime)EndExpenceDate;
            string sd = (reportResponse.StartTheDate).ToString("yyyy-MM-dd");
            string ed = (reportResponse.EndTheDate).ToString("yyyy-MM-dd");
            reportResponse.RaporName = $"Report_{sd}-{ed} Arası Faliyet Raporu";


            return new ApiResponse<ReportResponse>(reportResponse);
        }
    }
}