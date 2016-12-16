# Mijika

Just a super easy way to get web jobs, job histories, invoking jobs...

You have to provide these configuration values:

### project

Name of the web app in Azure. Basically it is this part in Kudu URI:
``[project].scm.azurewebsites.net``

### username / password

The easies way is to navigate to *Web jobs* blade in your web app, choose your web jobs
and open *Properties*. There is a web hook info. Copy *User name* and *Password* and voila.

Alternatively just download publish settings a copy values from XML file.

## Methods

- GetWebJobs
- GetTriggeredWebJobs
- GetContinuousWebJobs
- GetTriggeredWebJob
- GetContinuousWebJob
- GetTriggeredWebJobHistory
- InvokeTriggeredJob
- GetLog

## Sample

```cs
var kudu = new Kudu(project, username, password);            

var jobs = kudu.GetWebJobs();

foreach (var job in jobs)
{
    Console.Out.WriteLine($"* {job.Name} [{job.Type}]");

    if (job.LatestRun != null)
    {
        Console.Out.WriteLine($"  Latest run: {job.LatestRun.Id} [{job.LatestRun.Status}] at {job.LatestRun.StartTime} ({job.LatestRun.Duration})");
    }
}

var result = kudu.InvokeTriggeredJob(jobs[0].Name, "test=true");
Console.Out.WriteLine($"  Location: {result}");
```