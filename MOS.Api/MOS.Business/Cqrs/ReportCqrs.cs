using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MOS.Base.Response;
using MOS.Schema;

namespace MOS.Business.Cqrs;

public record GetReportQuery(ReportRequest Model) : IRequest<ApiResponse<ReportResponse>>;