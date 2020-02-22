using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcAppService.Services
{
    public class MyService:MyGrpcService.MyGrpcServiceBase
    {
        public override Task<MyResponse> Classes(MyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new MyResponse { Message = "hello" });
        }
    }
}
