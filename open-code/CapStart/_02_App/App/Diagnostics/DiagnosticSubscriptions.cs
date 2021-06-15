using DotNetCore.CAP.Diagnostics;
using DotNetCore.CAP.Messages;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Reflection;
using System.Text.Json;

namespace App.Diagnostics
{
    public class DiagnosticSubscriptions
    {
        [DiagnosticName(CapDiagnosticListenerNames.BeforePublishMessageStore)]
        public void BeforePublishStore(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.AfterPublishMessageStore)]
        public void AfterPublishStore(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.ErrorPublishMessageStore)]
        public void ErrorPublishStore(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.BeforePublish)]
        public void BeforePublish(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.AfterPublish)]
        public void AfterPublish(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.ErrorPublish)]
        public void ErrorPublish(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }


        [DiagnosticName(CapDiagnosticListenerNames.BeforeConsume)]
        public void CapBeforeConsume(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.AfterConsume)]
        public void CapAfterConsume(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.ErrorConsume)]
        public void CapErrorConsume(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.BeforeSubscriberInvoke)]
        public void CapBeforeSubscriberInvoke(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.AfterSubscriberInvoke)]
        public void CapAfterSubscriberInvoke(object eventData)
        {
            Console.WriteLine(JsonSerializer.Serialize(eventData));
        }

        [DiagnosticName(CapDiagnosticListenerNames.ErrorSubscriberInvoke)]
        public void CapErrorSubscriberInvoke(long? operationTimestamp, string operation, Message message, MethodInfo methodInfo, long? elapsedTimeMs, Exception exception)
        {
            Console.WriteLine(JsonSerializer.Serialize(new
            {
                OperationTimestamp = operationTimestamp,
                Operation = operation,
                Message = message,
                MethodInfo = methodInfo.Name,
                ElapsedTimeMs = elapsedTimeMs,
                Exception = exception.ToString()
            }));
        }
    }
}
