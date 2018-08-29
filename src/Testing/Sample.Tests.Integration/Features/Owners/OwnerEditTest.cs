using System.Threading.Tasks;

namespace Sample.Tests.Integration.Features.Owners
{
    using Sample.Core.Domain;
    using Sample.Web.Features.Owners;
    using Shouldly;
    using Xunit;
    using static SliceFixture;

    public class OwnerEditTest : IntegrationTestBase
    {
        [Fact]
        public async Task Should_edit_owner()
        {
            //Arrange
            var createdId = await EntitySetupHelper.CreateOwner().ConfigureAwait(false);
            var command = new Edit.Command
            {
                Id = createdId,
                Name = "Foo"
            };

            //Act
            await SendAsync(command).ConfigureAwait(false);
            var edited = await FindAsync<Owner>(createdId);

            //Assert
            edited.ShouldNotBeNull();
            edited.Name.ShouldBe("Foo");
        }
    }
}
