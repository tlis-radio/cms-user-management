using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserUpdateProfileImageRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserUpdateProfileImageRequest, bool>
{
    public async Task<bool> Handle(UserUpdateProfileImageRequest request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.UserRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.ProfileImageId = request.ProfileImageId;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}