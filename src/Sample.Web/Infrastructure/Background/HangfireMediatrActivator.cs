using Hangfire;
using MediatR;
using System;

namespace Sample.Web.Infrastructure.Background
{
    public class HangfireMediatrActivator : JobActivator
    {
        private readonly IMediator _mediator;

        public HangfireMediatrActivator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override object ActivateJob(Type jobType)
        {
            return new HangfireMediator(_mediator);
        }
    }
}
