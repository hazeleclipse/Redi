namespace Redi.WebUi.Requests.ProfitLedger
{
    public record CreateDailyProfitRequest(DateOnly Date, decimal Profit);
}
