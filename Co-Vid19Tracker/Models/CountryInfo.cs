using System.Collections.Generic;

namespace Co_Vid19Tracker.Models
{
    internal class CountryInfo : PlaceInfo
    { 
        public IEnumerable<ProvinceInfo> ProvinceCount { get; set; }
    }
}
