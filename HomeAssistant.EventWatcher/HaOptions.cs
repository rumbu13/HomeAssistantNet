using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistant.EventWatcher;

public class HaOptions
{
    public string? Host { get; init; }
    public int? Port { get; init; }
    public bool? UseSsl { get; init; }
    public string? Token { get; init; }
}
