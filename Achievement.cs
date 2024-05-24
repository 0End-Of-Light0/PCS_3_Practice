using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Achievement
{
    public long AchievementId { get; set; }

    public string? NameAchievement { get; set; }

    public decimal? Bonus { get; set; }

    public virtual ICollection<EnrolleeAchievement> EnrolleeAchievements { get; set; } = new List<EnrolleeAchievement>();
}
