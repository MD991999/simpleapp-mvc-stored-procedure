using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace products_crud_application.Models
{
    public class product
    {
        //here we declare the properties of our table column
        [Key] //since the id is primatry key
        public int ProductID { get; set; }
        [Required]
   //   [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public string Remarks { get; set; }
    }
}