using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class ProgramSubject
{
    public long ProgramSubjectId { get; set; }

    public long? ProgramId { get; set; }

    public long? SubjectId { get; set; }

    public decimal? MinResult { get; set; }

    public virtual Program? Program { get; set; }

    public virtual Subject? Subject { get; set; }
}
