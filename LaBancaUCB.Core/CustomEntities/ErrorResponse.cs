using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.CustomEntities;
public class ErrorResponse
{
    public int Status { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public IEnumerable<object>? Errors { get; set; }
    public string? TraceId { get; set; }
}
