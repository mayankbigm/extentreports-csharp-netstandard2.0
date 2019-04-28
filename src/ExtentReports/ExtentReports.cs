using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AventStack.ExtentReports
{
    public class ExtentReports : ExtentObservable, IReportService
    {
        public List<IExtentReporter> StartedReporterList => StarterReporterList;

        public string GherkinDialect
        {
            get => string.IsNullOrEmpty(GherkinDialect) ? GherkinDialectProvider.Language : GherkinDialect;
            set => GherkinDialectProvider.Language = value;
        }

        public new AnalysisStrategy AnalysisStrategy
        {
            get => base.AnalysisStrategy;
            set => base.AnalysisStrategy = value;
        }

        public Status Status => ReportStatus;

        public ReportStatusStats Stats => ReportStatusStats;

        public void AttachReporter(params IExtentReporter[] reporter)
        {
            reporter.ToList().ForEach(Register);
        }

        public ExtentTest CreateTest<T>(string name, string description = "") where T : IGherkinFormatterModel
        {
            Type type = typeof(T);
            var obj = (IGherkinFormatterModel)Activator.CreateInstance(type);

            var test = new ExtentTest(this, obj, name, description);
            SaveTest(test.Model);

            return test;
        }

        public ExtentTest CreateTest(GherkinKeyword keyword, string name, string description = "")
        {
            var test = new ExtentTest(this, name, description) {Model = {BehaviorDrivenType = keyword.Model}};
            SaveTest(test.Model);
            return test;
        }

        public ExtentTest CreateTest(string name, string description = "")
        {
            var test = new ExtentTest(this, name, description);
            SaveTest(test.Model);
            return test;
        }

        public void RemoveTest(ExtentTest test)
        {
            RemoveTest(test.Model);
        }

        public new void Flush()
        {
            base.Flush();
        }

        public new void AddSystemInfo(string name, string value)
        {
            base.AddSystemInfo(name, value);
        }

        /// <summary>
        /// Adds logs from test framework tools to the test-runner logs view (if available in the reporter)
        /// </summary>
        /// <param name="log"></param>
        public new void AddTestRunnerLogs(string log)
        {
            base.AddTestRunnerLogs(log);
        }

        /// <summary>
        /// Adds logs from test framework tools to the test-runner logs view (if available in the reporter)
        /// </summary>
        /// <param name="log"></param>
        public void AddTestRunnerLogs(string[] log)
        {
            base.AddTestRunnerLogs(log.ToList());
        }

        /// <summary>
        /// Adds logs from test framework tools to the test-runner logs view (if available in the reporter)
        /// </summary>
        /// <param name="log"></param>
        public new void AddTestRunnerLogs(List<string> log)
        {
            base.AddTestRunnerLogs(log);
        }
    }
}
