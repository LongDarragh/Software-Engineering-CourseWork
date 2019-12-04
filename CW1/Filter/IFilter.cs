using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW1
{

    /*The main Filter for the SMS, Email, Tweet 
     * And check the input is it much the format from the Dictionary
     */
    public interface IFilter
    {
        Dictionary<string, string> StartFilter();
    }
}
