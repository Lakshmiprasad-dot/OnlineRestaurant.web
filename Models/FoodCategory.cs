using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineRestaurant.Web.Models
{
    [Table(name: "Food_Categories")]
    public class FoodCategory
    {
        [Key]                                                       // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       // Identity Column
        [Display(Name = "Food Category ID")]
        public int FoodCategoryId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Name of the Food Category")]
        public string FoodCategoryName { get; set; }


        #region Navigation Properties to the Food Model

        public ICollection<Food> Foods { get; set; }

        #endregion
    }
}
