using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class EnrolleeSubject
{
    public long EnrolleeSubjectId { get; set; }

    public long? EnrolleeId { get; set; }

    public long? SubjectId { get; set; }

    public decimal? Result { get; set; }

    public virtual Enrollee? Enrollee { get; set; }

    public virtual Subject? Subject { get; set; }
}
