using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class ProgramEnrollee
{
    public long ProgramEnrolleeId { get; set; }

    public long? ProgramId { get; set; }

    public long? EnrolleeId { get; set; }

    public virtual Enrollee? Enrollee { get; set; }

    public virtual Program? Program { get; set; }
}
