﻿using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class TermDays : BaseEntity
{
    public int Days { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public string AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsActive { get; set; }
}