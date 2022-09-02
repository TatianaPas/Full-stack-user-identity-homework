using System;
using System.Collections.Generic;

namespace UserIdentityHomework.Models.DB
{
    public partial class TruckFeature
    {
        public TruckFeature()
        {
            Trucks = new HashSet<IndividualTruck>();
        }

        public int FeatureId { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<IndividualTruck> Trucks { get; set; }
    }
}
