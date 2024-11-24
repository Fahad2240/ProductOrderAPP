using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DotNET.Models
{
    public class TblOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intOrderId { get; set; }

        [ForeignKey("TblProducts")]
        public int IntProductId { get; set; }
        public TblProducts  TblProducts  { get; set; } // Navigation property

        [Column(TypeName = "nvarchar(max)")]
        public string strCustomerName { get; set; }

   
        [Column(TypeName = "decimal(18, 2)")]
        public decimal numQuantity { get; set; }

        [Required]
        public DateTime dtOrderDate { get; set; }
        
    }
}
