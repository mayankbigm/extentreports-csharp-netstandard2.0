using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.MarkupUtils;

using MongoDB.Bson;

using System;

namespace AventStack.ExtentReports.Model
{
    [Serializable]
    public class Log : IBasicMongoReportElement, IRunResult
    {
        public DateTime Timestamp { get => _timestamp; set => _timestamp = value; }
        public Status Status { get; set; }
        public ObjectId ObjectId { get => _objectId; set => _objectId = value; }
        public Test Test { get; private set; }
        public string Details { get; set; } = string.Empty;
        public int Sequence { get; set; } = 0;
        public IMarkup Markup { get; set; }
        public ExceptionInfo ExceptionInfo { get; set; }

        public GenericStructure<ScreenCapture> ScreenCaptureContext { get; set; }

        [NonSerialized]
        private DateTime _timestamp = DateTime.Now;
        [NonSerialized]
        private ObjectId _objectId;

        public Log(ExtentTest test)
        {
            Test = test.Model;
        }

        public Log(Test test)
        {
            Test = test;
        }

        public void AddScreenCapture(ScreenCapture screenCapture)
        {
            if (ScreenCaptureContext == null)
            {
                ScreenCaptureContext = new GenericStructure<ScreenCapture>();
            }
            ScreenCaptureContext.Add(screenCapture);
            screenCapture.TestObjectId = Test.ObjectId;
        }

        public bool HasException => ExceptionInfo != null;

        public bool HasScreenCapture => ScreenCaptureContext != null;
    }
}