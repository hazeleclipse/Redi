using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.ProfitLedgers.Commands.CalculateEndOfDayProfits;
using Redi.Application.ProfitLedgers.Queries.GetFull;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities;
using Redi.WebUi.Requests.ProfitLedger;

namespace Redi.WebUi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;

        public IndexModel(ISender mediator)
        {
            _mediator = mediator;
        }

        public IEnumerable<DailyCompanyProfitEntry> DailyCompanyProfits { get; set; } = default!;

        [BindProperty]
        public CreateDailyProfitRequest CreateDailyProfitRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var ledger = await _mediator.Send(new GetFullProfitLedger());

            DailyCompanyProfits = ledger.CompanyProfits; 

            return Page();
        }

        public async Task<IActionResult> OnPostCreateProfitAsync()
        {
            if (CreateDailyProfitRequest is null)
                throw new Exception("Create daily profit request is invalid");

            var calculateProfits = new CalculateEndOfDayProfits(CreateDailyProfitRequest.Date, CreateDailyProfitRequest.Profit);
            await _mediator.Send(calculateProfits);

            return RedirectToPage();
        }
    }
}