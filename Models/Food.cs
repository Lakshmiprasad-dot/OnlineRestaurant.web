using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineRestaurant.Web.Models
{
    [Table(name: "Foods")]
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int FoodId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Food Name")]
        virtual public string FoodName { get; set; }

        [Required]
        [DefaultValue(1)]
        [Display(Name = "Food Quantity")]
        virtual public int Quantity { get; set; }

        [Required]
        [Display(Name = "Food Price")]
        public int Price { get; set; }

        [Required]
        [DefaultValue(false)]
        [Display(Name = "Food Is Enabled")]
        virtual public bool IsEnabled { get; set; }

        #region Navigation Properties to the Category Model
        virtual public int FoodCategoryId { get; set; }

        [ForeignKey(nameof(Food.FoodCategoryId))]
        [Display(Name = "Food Category")]
        public FoodCategory FoodCategory { get; set; }

        #endregion
    }
}
