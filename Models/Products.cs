using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace DotNET.Models
{
    public class TblProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntProductId { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string strProductName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal numUnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal numStock { get; set; }
        public ICollection<TblOrders>TblOrders { get; set; } // Navigation property

    }
}
