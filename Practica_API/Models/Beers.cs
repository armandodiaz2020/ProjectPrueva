﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Practica_API.Models;
[Table("Beers", Schema = "dbo")]


public partial class Beers
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }



