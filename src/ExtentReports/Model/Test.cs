using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Gherkin.Model;

using MongoDB.Bson;
using System;
using System.Linq;
using System.Threading;

namespace AventStack.ExtentReports.Model
{
    [Serializable]
    public class Test : IBasicMongoReportElement, IRunResult
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TestId { get; } = Interlocked.Increment(ref _counter);
        public ObjectId ObjectId { get; set; }
        public Status Status { get; set; } = Status.Pass;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;
        public IGherkinFormatterModel BehaviorDrivenType { get; set; }
        public ExtentReports Extent { get; set; }
        public Test Parent { get; set; }

        public string BehaviorDrivenTypeName => IsBehaviorDrivenType ? BehaviorDrivenType.ToString() : null;

        public int Level => Parent?.Level + 1 ?? 0;

        public string HierarchicalName {
            get
            {
               if (Parent != null)
                    return Parent.Name + "." + Name;
                return Name;
            }
        }

        private static int _counter;
        private GenericStructure<Log> _logContext;
        private GenericStructure<Test> _nodeContext;
        private GenericStructure<TestAttribute> _authorContext;
        private GenericStructure<TestAttribute> _categoryContext;
        private GenericStructure<TestAttribute> _deviceContext;
        private GenericStructure<TestAttribute> _exceptionInfoContext;
        private GenericStructure<ScreenCapture> _screenCaptureContext;

        private readonly object _syncLock = new object();

        public bool IsChild => Level > 0;

        public TimeSpan RunDuration => EndTime.Subtract(StartTime);

        public GenericStructure<Test> NodeContext => _nodeContext ?? (_nodeContext = new GenericStructure<Test>());

        public bool HasChildren => NodeContext?.All() != null && NodeContext.Count > 0;

        public GenericStructure<Log> LogContext => _logContext ?? (_logContext = new GenericStructure<Log>());

        public bool HasLog => LogContext?.All() != null && LogContext.Count > 0;

        public bool HasAttributes => HasAuthor || HasCategory || HasDevice;

        public GenericStructure<TestAttribute> CategoryContext => _categoryContext ?? (_categoryContext = new GenericStructure<TestAttribute>());

        public bool HasCategory => CategoryContext != null && CategoryContext.Count > 0;

        public GenericStructure<TestAttribute> ExceptionInfoContext => _exceptionInfoContext ?? (_exceptionInfoContext = new GenericStructure<TestAttribute>());

        public bool HasException => ExceptionInfoContext != null && ExceptionInfoContext.Count > 0;

        public GenericStructure<TestAttribute> AuthorContext => _authorContext ?? (_authorContext = new GenericStructure<TestAttribute>());

        public bool HasAuthor => AuthorContext != null && AuthorContext.Count > 0;

        public GenericStructure<TestAttribute> DeviceContext => _deviceContext ?? (_deviceContext = new GenericStructure<TestAttribute>());

        public bool HasDevice => DeviceContext != null && DeviceContext.Count > 0;

        public GenericStructure<ScreenCapture> ScreenCaptureContext => _screenCaptureContext ?? (_screenCaptureContext = new GenericStructure<ScreenCapture>());

        public bool HasScreenCapture => _screenCaptureContext != null && _screenCaptureContext.Count > 0;

        public bool IsBehaviorDrivenType => BehaviorDrivenType != null;

        public void End()
        {
            lock (_syncLock)
            {
                UpdateTestStatusRecursive(this);
                EndChildTestsRecursive(this);
                Status = (Status == Status.Info || Status == Status.Debug) ? Status.Pass : Status;
                SetEndTimeFromChildren();
            }
        }

        private void SetEndTimeFromChildren()
        {
            if (HasChildren)
            {
                EndTime = NodeContext.LastOrDefault().EndTime;
            }
            else if (HasLog)
            {
                EndTime = LogContext.LastOrDefault().Timestamp;
            }
        }

        private void UpdateTestStatusRecursive(Test test)
        {
            if (test.HasLog)
            {
                test.LogContext.All().ToList().ForEach(x => UpdateStatus(x.Status));
            }

            if (test.HasChildren)
            {
                test.NodeContext.All().ToList().ForEach(UpdateTestStatusRecursive);
            }

            if (test.IsBehaviorDrivenType) return;
            {
                bool hasNodeNotSkipped = test.NodeContext.All().Any(x => x.Status != Status.Skip);
                if (this.Status != Status.Skip || !hasNodeNotSkipped) return;
                {
                    Status = Status.Pass;
                    test.NodeContext.All()
                        .FindAll(x => x.Status != Status.Skip)
                        .ForEach(UpdateTestStatusRecursive);
                }
            }
        }

        private void UpdateStatus(Status status)
        {
            var statusIndex = StatusHierarchy.GetStatusHierarchy().IndexOf(status);
            var testStatusIndex = StatusHierarchy.GetStatusHierarchy().IndexOf(Status);
            lock (_syncLock)
            {
                Status = statusIndex < testStatusIndex ? status : Status;
            }
        }
        
        private void EndChildTestsRecursive(Test test)
        {
            if (!test.HasChildren) return;
            lock (_syncLock)
            {
                test.NodeContext.All().ToList().ForEach(x => x.End());
            }
        }
    }
}
