﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication_Shared_Services.Service
{
    public interface IOperation
    {
        string OperationId { get; }
    }

    public interface IOperationTransient : IOperation { }
    public interface IOperationScoped : IOperation { }
    public interface IOperationSingleton : IOperation { }

    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Operation()
        {
            OperationId = Guid.NewGuid().ToString();
        }

        public string OperationId { get; }
    }

}
