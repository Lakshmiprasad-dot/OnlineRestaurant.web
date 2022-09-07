using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineRestaurant.Web.Models
{
    [Table(name: "Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        virtual public int OrderId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Food Order Name")]
        virtual public string OrderedFoodName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Order Date")]
        public DateTime OrderDateTime { get; set; }
        [Required]
        [DefaultValue(1)]
        [Display(Name = "Order Quantity")]
        virtual public int OrderedQuantity { get; set; }
        [Required]
        [Display(Name = "Customer Name ")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Customer Email ")]
        public string CustomerEmail { get; set; }

        #region Navigation Properties to the Order Status Model

        public ICollection<OrderStatus> OrderStatuses { get; set; }

        #endregion

        #region Navigation Properties to the Food Model
        virtual public int FoodCategoryId { get; set; }

        [ForeignKey(nameof(Order.FoodCategoryId))]
        public FoodCategory FoodCategory { get; set; }

        #endregion

       
    }
}
