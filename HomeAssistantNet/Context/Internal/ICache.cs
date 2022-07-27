using HomeAssistantNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Context.Internal;

public interface ICache: IDisposable
{
    Task WaitForLoadAsync();
    bool IsLoaded { get; }
}

public interface ICache<T> : ICache where T : class
{
    IEnumerable<T> GetItems();
    T? GetItem(string key);
   
}
