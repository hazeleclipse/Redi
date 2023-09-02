using MediatR;
using Redi.Domain.Aggregates.ProfitLedgerAggregate;

namespace Redi.Application.ProfitLedgers.Queries.GetFull
{
    public record GetFullProfitLedger() : IRequest<ProfitLedger>;
}
