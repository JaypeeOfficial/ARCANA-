﻿using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Department : BaseEntity
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}