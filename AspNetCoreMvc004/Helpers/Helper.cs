using AspNetCoreMvc004.Models;
using Microsoft.Identity.Client.Extensibility;

namespace AspNetCoreMvc004.Helpers
{
    public class Helper : IHelper
    {

        public string Upper(string text)
        { 
            return text.ToUpper();
        }
    }
}
