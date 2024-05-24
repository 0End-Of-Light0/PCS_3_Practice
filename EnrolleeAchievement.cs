using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class EnrolleeAchievement
{
    public long EnrolleeAchivId { get; set; }

    public long? EnrolleeId { get; set; }

    public long? AchievementId { get; set; }

    public virtual Achievement? Achievement { get; set; }

    public virtual Enrollee? Enrollee { get; set; }
}
