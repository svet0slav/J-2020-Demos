using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryConfirmation.Shared.Entities.Entities
{
    public partial class Trucks
    {
        public Trucks()
        {
            Shipments = new HashSet<Shipments>();
            CreationDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TruckId { get; set; }
        public string TruckNumber { get; set; }
        public string TruckModel { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public virtual ICollection<Shipments> Shipments { get; set; }
        
    }
}