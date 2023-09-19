using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Practica_API.Models;
    [Table("Customers", Schema ="dbo")]

    public partial class Customer
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public string? LastName { get; set; }
       
        public string? Address { get; set; }
       
        public string? Phone { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }

