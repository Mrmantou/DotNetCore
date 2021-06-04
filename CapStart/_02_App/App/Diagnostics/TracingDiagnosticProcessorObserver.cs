using System;
using System.Diagnostics;

namespace App.Diagnostics
{
    public class TracingDiagnosticProcessorObserver : IObserver<DiagnosticListener>
    {

        public TracingDiagnosticProcessorObserver()
        {

        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener listener)
        {
            listener.SubscribeWithAdapter(new DiagnosticSubscriptions(), name => true);
        }
    }
}
