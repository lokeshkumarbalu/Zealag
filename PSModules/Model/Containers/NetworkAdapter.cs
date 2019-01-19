//------------------------------------------------------------------------------
// <copyright file="NetworkAdapter.cs" company="Zealag">
//    Copyright © Zealag 2018. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Zealag.PSModules.Model.Containers
{
    public class NetworkAdapter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DeviceId { get; set; }
        public string Manufacturer { get; set; }
        public string NetConnectionId { get; set; }
        public bool PhysicalAdapter { get; set; }
    }
}
