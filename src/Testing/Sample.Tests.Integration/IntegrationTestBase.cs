using Nito.AsyncEx;
using System.Threading.Tasks;
using Xunit;

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
