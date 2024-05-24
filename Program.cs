using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Program
{
    public long ProgramId { get; set; }

    public string? NameProgram { get; set; }

    public long? DepartmentId { get; set; }

    public long? Plan { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<ProgramEnrollee> ProgramEnrollees { get; set; } = new List<ProgramEnrollee>();

    public virtual ICollection<ProgramSubject> ProgramSubjects { get; set; } = new List<ProgramSubject>();

    static void Main(string[] args)
    {
        // получение данных
        using (PostgresContext db = new PostgresContext())
        {
            // получаем объекты из бд и выводим на консоль
            Console.WriteLine("Задание1----------------------------------");
            var programName = "Самолетостроение";

            var enrollees1 = db.ProgramEnrollees
                .Where(pe => pe.Program.NameProgram == programName)
                .Select(pe => pe.Enrollee)
                .ToList();

            foreach (var enrollee in enrollees1)
            {
                Console.WriteLine($"Имя: {enrollee.NameEnrollee}");
            }
            Console.WriteLine("Задание2----------------------------------");
            var SubjectName = "Физика";
            var programs2 = db.ProgramSubjects
                .Where(ps => ps.Subject.NameSubject == SubjectName)
                .Select(ps => ps.Program)
                .ToList();
            foreach (var program in programs2)
            {
                Console.WriteLine($"Имя: {program.NameProgram}");
            }
            Console.WriteLine("Задание3----------------------------------");
            var subjects3 = db.EnrolleeSubjects
                .GroupBy(es => es.SubjectId)
                .Select(group => new
                {
                    SubjectName = group.Key,
                    MinResult = group.Min(es => es.Result),
                    MaxResult = group.Max(es => es.Result),
                    Count = group.Count()
                })
                .OrderBy(x => x.SubjectName)
                .ToList();
            foreach (var subject in subjects3)
            {
                Console.WriteLine($"Название предмета: {subject.SubjectName}, Минимальный результат: {subject.MinResult}, Максимальный результат: {subject.MaxResult}, Количество: {subject.Count}");
            }
            Console.WriteLine("Задание4----------------------------------");
            var programs4 = db.ProgramSubjects
                .Where(ps => ps.Subject.EnrolleeSubjects.Any(pss => pss.Result >= ps.MinResult))
                .Select(ps => ps.Program)
                .Distinct()
                .ToList();
            foreach (var program in programs4)
            {
                Console.WriteLine($"Название программы: {program.NameProgram}");
            }
            Console.WriteLine("Задание5----------------------------------");
            var programs5 = db.Programs
                .OrderByDescending(p => p.Plan)
                .Select(p => p)
                .FirstOrDefault();
            Console.WriteLine($"Название программы: {programs5.NameProgram}, План набора: {programs5.Plan}");
            Console.WriteLine("Задание6----------------------------------");
            var achivment6 = db.EnrolleeAchievements
                .GroupBy(ea => ea.EnrolleeId)
                .Select(group => new
                {
                    EnrolleeName = group.Key,
                    AchivSum = group.Sum(ea => ea.Achievement.Bonus),
                    Count = group.Count()
                })
                .OrderBy(x => x.EnrolleeName)
                .ToList();
            foreach (var achiv in achivment6)
            {
                Console.WriteLine($"Имя абитуриента: {achiv.EnrolleeName}, Сумма бонусных баллов: {achiv.AchivSum}, количество достижений: {achiv.Count}");
            }
            Console.WriteLine("Задание7----------------------------------");
            var programs7 = db.ProgramEnrollees
                .GroupBy(pe => pe.ProgramId)
                .Select(group => new
                {
                    ProgramName = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.ProgramName)
                .ToList();
            foreach (var program in programs7)
            {
                Console.WriteLine($"Программа: {program.ProgramName}, Конкурс: {program.Count}");
            }
            Console.WriteLine("Задание8----------------------------------");
            var subj1 = "Физика";
            var subj2 = "Математика П.";
            var programs8 = db.ProgramSubjects
                .Where(ps => ps.SubjectId == db.Subjects.Where(s => s.NameSubject == subj1).Select(s => s.SubjectId).FirstOrDefault() || ps.SubjectId == db.Subjects.Where(s => s.NameSubject == subj2).Select(s => s.SubjectId).FirstOrDefault())
                .GroupBy(ps => ps.Program.NameProgram)
                .Where(g => g.Count() >= 2)
                .Select(g => g.Key)
                .Distinct()
                .ToList();
            foreach (var program in programs8)
            {
                Console.WriteLine($"Программа: {program}");
            }
            Console.WriteLine("Задание9----------------------------------");
            var results9 = (from es in db.EnrolleeSubjects
                           join ps in db.ProgramSubjects on es.SubjectId equals ps.SubjectId
                           join p in db.Programs on ps.ProgramId equals p.ProgramId
                           group es by new { es.EnrolleeId, p.NameProgram } into g
                           select new
                           {
                               EnrolleeId = g.Key.EnrolleeId,
                               ProgramName = g.Key.NameProgram,
                               TotalScore = g.Sum(es => es.Result)
                           });
            foreach (var result in results9)
            {
                Console.WriteLine($"Имя: {result.EnrolleeId}, Программа: {result.ProgramName}, Баллы: {result.TotalScore}");
            }
            Console.WriteLine("Задание10----------------------------------");
            /*
            var applicants10 = from e in db.Enrollees
                             join es in db.EnrolleeSubjects on e.EnrolleeId equals es.EnrolleeId
                             join ps in db.ProgramSubjects on es.SubjectId equals ps.SubjectId
                             join p in db.Programs on ps.ProgramId equals p.ProgramId
                             group es by e.EnrolleeId into g
                             select new
                             {
                                 EnrolleeId = g.Key,
                                 MinimumResult = g.Max(es => es.Result),
                                 CountHigherScorers = g.Count(es => es.Result >= g.Max(es => es.Result))
                             };
            var nonEnrollableApplicants = applicants10.Where(a => a.CountHigherScorers > 0 || a.MinimumResult < );
            foreach (var result in nonEnrollableApplicants)
            {
                Console.WriteLine($"Имя: {result}");
            }
            */
        }
    }
}