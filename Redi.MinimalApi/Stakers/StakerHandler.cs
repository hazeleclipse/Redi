using MediatR;
using Redi.Application.Stakers.Commands.Delete;
using Redi.Application.Stakers.Commands.Register;
using Redi.Application.Stakers.Queries.GetAll;
using Redi.Application.Stakers.Queries.GetById;

namespace Redi.MinimalApi.Stakers
{
    internal static class StakerHandler
    {
        internal static async Task<IResult> CreateStaker(ISender mediatr)
        {
            var createStaker = new RegisterStaker();

            var newStakerDto = await mediatr.Send(createStaker);

            return TypedResults.Created($"/api/stakers/{newStakerDto.Id}", newStakerDto);
        }

        internal static async Task<IResult> DeleteStaker(Guid id, ISender mediatr)
        {
            var deleteStaker = new DeleteStaker(id);

            await mediatr.Send(deleteStaker);

            return TypedResults.NoContent();
        }

        internal static async Task<IResult> GetAllStakers(ISender mediatr)
        {
            var getAllStakers = new GetAllStakers();

            var stakerDtos = await mediatr.Send(getAllStakers);

            return TypedResults.Ok(stakerDtos);
        }

        internal static async Task<IResult> GetStakerById(Guid id, ISender mediatr)
        {
            var getStakerById = new GetStakerById(id);

            var stakerDto = await mediatr.Send(getStakerById);

            return TypedResults.Ok(stakerDto);
        }
    }
}
