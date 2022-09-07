using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineRestaurant.Web.Models
{
    [Table(name: "Order_Status")]
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderStatusId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "Order Id")]
        public int FoodOrderId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "Order Status")]
        public string Status { get; set; }


        #region Navigation Properties to the Orders Model
        virtual public int OrderId { get; set; }

        [ForeignKey(nameof(OrderStatus.OrderId))]
        
        public Order Order { get; set; }

        #endregion
    }
}
