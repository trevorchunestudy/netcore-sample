using Nito.AsyncEx;
using System.Threading.Tasks;
using Xunit;

//This forces each test class to run on the same thread which forces a 'synchronous' type execution keeping the database clean after each test.
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
namespace Sample.Tests.Integration
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly AsyncLock Mutex = new AsyncLock();

        //private static bool _initialized;

        public virtual async Task InitializeAsync()
        {
            //uncomment to run once per test run
            //if (_initialized)
            //    return;

            using (await Mutex.LockAsync())
            {
                //if (_initialized)
                //    return;

                await SliceFixture.ResetCheckpoint().ConfigureAwait(false);

                //_initialized = true;
            }
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}
