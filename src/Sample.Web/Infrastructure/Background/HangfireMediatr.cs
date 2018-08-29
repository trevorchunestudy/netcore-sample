using Hangfire;
using MediatR;

namespace Sample.Web.Infrastructure.Background
{
    public static class HangfireMediatr
    {
        public static void Enqueue(this IMediator mediator, IRequest<Unit> request)
        {
            BackgroundJob.Enqueue<HangfireMediator>(m => m.SendCommand(request));
        }
    }

    public class HangfireMediator
    {
        private readonly IMediator _mediator;

        public HangfireMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void SendCommand(IRequest<Unit> request)
        {
            _mediator.Send(request);
        }
    }
}
