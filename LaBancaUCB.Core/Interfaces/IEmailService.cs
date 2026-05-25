using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaBancaUCB.Core.Interfaces;

public interface IEmailService
{
    Task EnviarComprobanteAsync(string emailDestino, string asunto, string cuerpoHtml);
}