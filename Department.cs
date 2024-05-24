using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Department
{
    public long DepartmentId { get; set; }

    public string? NameDepartment { get; set; }

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();
}
