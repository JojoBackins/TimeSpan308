using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
class Program
{
    static void Main(string[] args)
    {
        //TimeSpan
        //public TimeSpan(int hours, int minutes, int seconds);
        //public TimeSpan(int days, int hours, int minutes, int seconds);
        //public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds);
        //public TimeSpan(long ticks);

        //public static TimeSpan FromDays(double value);
        //public static TimeSpan FromHours (double value);
        //public static TimeSpan FromMinutes (double value);
        //public static TimeSpan FromSeconds (double value);
        //public static TimeSpan FromMilliseconds (double value);
        DateTime dt1 = new DateTime(200, 1, 1, 10, 20, 30, DateTimeKind.Local);
        DateTime dt2 = new DateTime(200, 1, 1, 10, 20, 30, DateTimeKind.Utc);
        Console.WriteLine(dt1 == dt2); //True
        DateTime local = DateTime.Now;
        DateTime utc = local.ToUniversalTime();
        Console.WriteLine(local == utc); //False

        DateTime d = new DateTime(2020, 12, 12);
        DateTime utc1 = DateTime.SpecifyKind(d, DateTimeKind.Utc);
        Console.WriteLine(utc1);

        DateTimeOffset local1 = DateTimeOffset.Now;
        DateTimeOffset utc2 = local1.ToUniversalTime();
        Console.WriteLine(local1.Offset);
        Console.WriteLine(utc2.Offset);
        Console.WriteLine(local1 == utc2);
        Console.WriteLine(local1.EqualsExact(utc2));
        //TimeZoneInfo

        TimeZone zone = TimeZone.CurrentTimeZone;
        Console.WriteLine(zone.StandardName);
        Console.WriteLine(zone.DaylightName);

        DaylightTime day = zone.GetDaylightChanges(2019); // специфичная инф. о летнем времени для заданного года
        Console.WriteLine(day.Start.ToString("M"));
        Console.WriteLine(day.End.ToString("M"));
        Console.WriteLine(day.Delta);

        TimeZoneInfo zone1 = TimeZoneInfo.Local;
        Console.WriteLine(zone1.StandardName);
        Console.WriteLine(zone1.DaylightName);

        DateTime dt11 = new DateTime(2019, 1, 1);
        DateTime dt22 = new DateTime(2019, 6, 1);
        Console.WriteLine(zone1.IsDaylightSavingTime(dt11));
        Console.WriteLine(zone1.IsDaylightSavingTime(dt22));
        Console.WriteLine(zone1.GetUtcOffset(dt11));
        Console.WriteLine(zone1.GetUtcOffset(dt22));

        TimeZoneInfo wa = TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time");
        Console.WriteLine(wa.Id);
        Console.WriteLine(wa.DisplayName);
        Console.WriteLine(wa.BaseUtcOffset);
        Console.WriteLine(wa.SupportsDaylightSavingTime);
        foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            Console.WriteLine(z.Id);
        Console.WriteLine();
        foreach (TimeZoneInfo.AdjustmentRule rule in wa.GetAdjustmentRules())
            Console.WriteLine("Rule: applies from " + rule.DateStart + " to " + rule.DateEnd);

        foreach (TimeZoneInfo.AdjustmentRule rule in wa.GetAdjustmentRules())
        {
            Console.WriteLine("Rule: applies from " + rule.DateStart + " to " + rule.DateEnd);
            Console.WriteLine(" Delta: " + rule.DaylightDelta);
            Console.WriteLine(" Start: " + FormatTransitionTime(rule.DaylightTransitionStart, false));
            Console.WriteLine(" End: " + FormatTransitionTime(rule.DaylightTransitionEnd, true));
        }


    }
    static string FormatTransitionTime(TimeZoneInfo.TransitionTime tt, bool endTime)
    {
        if (endTime && tt.IsFixedDateRule && tt.Day == 1 && tt.Month == 1 && tt.TimeOfDay == DateTime.MinValue)
            return "-";
        string s;
        if (tt.IsFixedDateRule)
            s = tt.Day.ToString();
        else
            s = "The " + "first second third fourth last".Split()[tt.Week - 1] + " " + tt.DayOfWeek + " in";
        return s + " " + DateTimeFormatInfo.CurrentInfo.MonthNames[tt.Month - 1] + " at " + tt.TimeOfDay.TimeOfDay;
    }




}