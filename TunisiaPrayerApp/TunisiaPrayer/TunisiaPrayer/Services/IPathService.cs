using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TunisiaPrayer.Services
{
    public interface IPathService
    {
        //string InternalFolder { get; }
        string PublicExternalFolder { get; }
        //void install(string filePath);
        //string PrivateExternalFolder { get; }
    }
}