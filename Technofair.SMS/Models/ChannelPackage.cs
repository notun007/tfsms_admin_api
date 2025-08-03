using Newtonsoft.Json;
using System.Linq;

namespace TFSMS.Admin.Models
{    

    public class RequestProcess
    {
        public Xml xml { get; set; }
        public Packagelist PackageList { get; set; }
    }

    public class Xml
    {
        public string version { get; set; }
        public string encoding { get; set; }
    }

    public class Packagelist
    {
        public string APIVersion { get; set; }
        public List<ChannelPackage> ChannelPackage { get; set; }
        public string Status { get; set; }
    }

    public class ChannelPackage
    {
        public string ArtNr { get; set; }
        public string PackageGroup { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Period { get; set; }
        public string InitDisPeriod { get; set; }
        public string DisPeriod { get; set; }
        public string ChannelDefinitionPackage { get; set; }
        public string NumberOfChannels { get; set; }
    }

    public class ChannelRequestProcess
    {
        public Xml xml { get; set; }
        public ChannelList ChannelList { get; set; }
    }
    public class ChannelList
    {
        public string APIVersion { get; set; }
        public List<IntegratorChannel> Channel { get; set; }
        public string Status { get; set; }
    }
    public class IntegratorChannel
    {
        public string ChannelID { get; set; }
        public string Value { get; set; }
        public string? PackageName { get; set; }
        public string? IntegratorName { get; set; }
    }


}
