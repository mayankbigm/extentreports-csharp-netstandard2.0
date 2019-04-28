using System;

namespace AventStack.ExtentReports.Model
{
    [Serializable]
    public abstract class TestAttribute : Attribute
    {
        protected TestAttribute(string name) : base(name) { }
    }
}
