﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Common
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; protected set; }

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            ErrorCode = error;
        }

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(bool isSuccess, T value, string error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static new Result<T> Failure(string error) => new(false, default, error);
    }
}
