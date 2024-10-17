using NoteFinder.ExternalInfo.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteFinder.ExternalInfo.Service
{
    public interface IApiService
    {
        Task<string> CallApiAsync(PerplexityApiConfiguration perplexityApiConfig);
    }
}
