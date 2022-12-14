using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lopuhhh.Models
{
    public partial class Product
    {
        private string? _image;
        private decimal _cost;
        public Product()
        {
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            ProductSales = new HashSet<ProductSale>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ProductTypeId { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image 
        { 
            get => _image; 
            set => _image = value; 
        }
        public int? ProductionPersonCount { get; set; }
        public int? ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent 
        { 
            get => _cost; 
            set => _cost = value; 
        }

        [NotMapped]
        public string ImagePath
        {
            get => (_image == string.Empty) || (_image == null)
                ? $"..\\Resources\\picture.png"
                : $"..\\Resources{_image.Replace("jpg", "jpeg")}";
            set => _image = value;
        }

        [NotMapped]
        public string Materials
        {
            get
            {
                if (ProductMaterials.Count == 0)
                    return "Материалов нет";

                StringBuilder sb = new StringBuilder();
                sb.Append("Материалы: ");

                foreach (var pm in ProductMaterials)
                    sb.Append($"{pm.Material.Title}, ");

                sb.Remove(sb.Length - 2, 2);
                return sb.ToString();
            }
        }

        [NotMapped]
        public decimal Cost
        {
            get
            {
                if (ProductMaterials.Count == 0)
                    return _cost;

                decimal cost = 0;
                foreach (var pm in ProductMaterials)
                    cost += Math.Ceiling((decimal)pm.Count) * pm.Material.Cost;

                return cost;
            }
        }

        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
