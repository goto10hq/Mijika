using System;
using Microsoft.Azure;
using Mijika;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var project = CloudConfigurationManager.GetSetting("project");
            var username = CloudConfigurationManager.GetSetting("username");
            var password = CloudConfigurationManager.GetSetting("password");

            var kudu = new Kudu(project, username, password);            

            var jobs = kudu.GetWebJobs();

            Console.Out.WriteLine($"- GetWebJobs {"-".PadRight(50, '-')}");

            foreach (var job in jobs)
            {
                Console.Out.WriteLine($"* {job.Name} [{job.Type}]");

                if (job.LatestRun != null)
                {
                    Console.Out.WriteLine($"  Latest run: {job.LatestRun.Id} [{job.LatestRun.Status}] at {job.LatestRun.StartTime} ({job.LatestRun.Duration})");
                }
            }

            foreach (var job in jobs)
            {
                if (job.Type.Equals("triggered"))
                {
                    Console.Out.WriteLine($"- GetWebJob: {job.Name} {"-".PadRight(50, '-')}");
                    var j = kudu.GetTriggeredWebJob(job.Name);
                    Console.Out.WriteLine($"  Triggered job: {j.Name}");

                    var history = kudu.GetTriggeredWebJobHistory(job.Name);

                    if (history != null)
                    {
                        Console.Out.WriteLine("  History:");

                        var first = true;

                        foreach (var h in history.Runs)
                        {
                            Console.Out.WriteLine($"  {h.Id} at {h.StartTime} for {h.Duration}");

                            if (first)
                            {
                                Console.Out.WriteLine($"- Output log for the latest one {"-".PadRight(50, '-')}");
                                var log = kudu.GetLog(h.OutputUrl);
                                Console.Out.WriteLine(log);
                                first = false;
                                Console.Out.WriteLine($"- Output log for the latest one {"-".PadRight(50, '-')}");
                            }
                        }
                    }
                }

                if (job.Type.Equals("continuous"))
                {
                    Console.Out.WriteLine($"- GetWebJob: {job.Name} {"-".PadRight(50, '-')}");
                    var j = kudu.GetContinuousWebJob(job.Name);
                    Console.Out.WriteLine($"  Continuous job: {j.Name}");
                }
            }

            Console.Out.WriteLine($"- InvokeWebJob {jobs[0].Name} {"-".PadRight(50, '-')}");
            var result = kudu.InvokeTriggeredJob(jobs[0].Name, "test=true;type=debug");
            Console.Out.WriteLine($"  Location: {result}");
        }
    }
}
