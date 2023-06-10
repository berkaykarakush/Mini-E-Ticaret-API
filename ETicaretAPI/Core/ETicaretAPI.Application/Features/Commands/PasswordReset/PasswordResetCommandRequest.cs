using MediatR;

namespace ETicaretAPI.Application.Features.Commands.PasswordReset
{
    public class PasswordResetCommandRequest: IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}