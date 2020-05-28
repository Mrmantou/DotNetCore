using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _Options_06
{
    public class TimedRefreshTokenSource<TOptions> : IOptionsChangeTokenSource<TOptions>
    {
        private OptionsChangeToken changeToken;

        public string Name { get; }

        public TimedRefreshTokenSource(TimeSpan interval, string name)
        {
            this.Name = name ?? Options.DefaultName;
            changeToken = new OptionsChangeToken();
            ChangeToken.OnChange(
                () => new CancellationChangeToken(new CancellationTokenSource(interval).Token),
                () =>
                {
                    var previous = Interlocked.Exchange(ref changeToken, new OptionsChangeToken());
                    previous.OnChange();
                });
        }

        public IChangeToken GetChangeToken() => changeToken;

        private class OptionsChangeToken : IChangeToken
        {
            private readonly CancellationTokenSource tokenSource;

            public OptionsChangeToken() => tokenSource = new CancellationTokenSource();

            public bool HasChanged => tokenSource.Token.IsCancellationRequested;

            public bool ActiveChangeCallbacks => true;

            public IDisposable RegisterChangeCallback(Action<object> callback, object state) => tokenSource.Token.Register(callback, state);

            public void OnChange() => tokenSource.Cancel();
        }
    }
}
