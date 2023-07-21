﻿using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Company : BaseEntity
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}