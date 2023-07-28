﻿using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Location : BaseEntity
{
    public string LocationName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public string AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}