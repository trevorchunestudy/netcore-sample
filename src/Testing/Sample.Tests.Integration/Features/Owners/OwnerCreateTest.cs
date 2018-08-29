using Shouldly;

namespace Sample.Tests.Integration.Features.Owners
{
    using Sample.Core.Domain;
    using System.Threading.Tasks;
    using Xunit;
    using static SliceFixture;
    public class OwnerCreateTest : IntegrationTestBase
    {
        [Fact]
        public async Task Should_create_owner()
        {
            //Arrange

            //Act
            var createdId = await EntitySetupHelper.CreateOwner().ConfigureAwait(false);
            var created = await FindAsync<Owner>(createdId);

            //Assert
            createdId.ShouldBeGreaterThan(0);
            created.ShouldNotBeNull();
        }
    }
}
