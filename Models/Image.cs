namespace PaintManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data;
    using System.Configuration;
    using System.Data.SqlClient;

    [Table("Image")]
    public partial class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageID { get; set; }

        public string ImagePath { get; set; }

        public byte[] Data { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal CostPrice { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }
        [Required]
        public string Size { get; set; }


    }
}