using MediatR;

namespace ETicaretAPI.Application.Features.Commands.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest: IRequest<VerifyResetTokenCommandResponse>
    {
        public string resetToken { get; set; }
        public string UserId { get; set; }
    }
}