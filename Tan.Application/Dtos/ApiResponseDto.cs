﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tan.Application.Dtos;

public record ApiResponseDto<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
