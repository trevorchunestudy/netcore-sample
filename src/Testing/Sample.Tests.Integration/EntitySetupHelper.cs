using Sample.Web.Features.Owners;
using System.Threading.Tasks;
using static Sample.Tests.Integration.SliceFixture;

namespace Sample.Tests.Integration
{
    public static class EntitySetupHelper
    {
        public static async Task<long> CreateOwner()
        {
            var command = new Create.Command
            {
                Name = "Mike Smith"
            };

            return await SendAsync(command).ConfigureAwait(false);
        }
    }
}
