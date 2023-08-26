using MediatR;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Application.ProfitLedgers.Commands.CalculateEndOfDayProfits
{
    public record CalculateEndOfDayProfits(DateOnly Date, Profit Profit) : IRequest;
}
