using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public class BaseEntity
{
  
    public virtual long Id { get; set; }
}
